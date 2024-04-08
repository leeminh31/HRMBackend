using AutoMapper;
using HRMBackend.DataAccess.ChiTietQuyBu;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.QuyBu;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.QuyBu.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.QuyBu;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Resources.DTO.QuyPhep.Response;
using Org.BouncyCastle.Asn1.Ocsp;
using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Resources.DTO.QuyBu.Request;

namespace HRMBackend.Services.QuyBu
{
    public class QuyBuService : BaseService, IQuyBuService
    {
        #region Property
        private readonly IChiTietQuyBuDAO _chiTietQuyBuDAO;
        private readonly IQuyBuDAO _quyBuDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IHopDongDAO _hopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuyBuService(IChiTietQuyBuDAO chiTietQuyBuDAO,
            IQuyBuDAO quyBuDAO,
            INhanVienDAO nhanVienDAO,
            IPhongBanDAO phongBanDAO,
            IHopDongDAO hopDongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyBuDAO = chiTietQuyBuDAO;
            this._quyBuDAO = quyBuDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
            this._hopDongDAO = hopDongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<QuyBuResponse>>> GetByYearAsync(SearchQuyBuRequest request)
        {
            var ngayDauTienNam = new DateTime(request.Nam, 1, 1);

            var ngayCuoiCungNam = new DateTime(request.Nam, 12, 31);

            var quyBuNam = await _chiTietQuyBuDAO.GetByYearAsync(request.Nam);

            var searchPhongBanRequest = new SearchPhongBanRequest();

            searchPhongBanRequest.TenPhongBan = request.TenPhongBan;

            var phongBan = await _phongBanDAO.GetByParamsAsync(searchPhongBanRequest);

            var nhanVienTheoTen = await _nhanVienDAO.GetByParamsAsync(request.MaNhanVien, null, null, null, request.TenNhanVien);


            var listQuyBuResponse = new List<QuyBuResponse>();

            if (nhanVienTheoTen.isSuccess && phongBan.hasValue)
            {
                foreach (var nhanVien in nhanVienTheoTen.data)
                {
                    var quyBuResponse = new QuyBuResponse();
                    quyBuResponse.PhongBan = phongBan.data.FirstOrDefault(pb => pb.MaPhongBan == nhanVien.MaPhongBan)?.TenPhongBan;
                    if (quyBuResponse.PhongBan == null)
                        continue;

                    quyBuResponse.MaNhanVien = nhanVien.MaNhanVien;
                    quyBuResponse.HoTen = nhanVien.HoTen;
                    quyBuResponse.Nam = request.Nam;

                    var hopDong = await _hopDongDAO.GetContractByYearAsync(nhanVien.MaNhanVien, request.Nam);

                    var phatSinh = 0;
                    var suDung = 0;
                    var conLai = 0;

                    var maQuyBus = await _quyBuDAO.GetByEmployeeIDAsync(nhanVien.MaNhanVien);
                    if (maQuyBus.isSuccess)
                    {
                        for (int thang = 1; thang <= 12; thang++)
                        {
                            quyBuResponse.QuyBuThangs[thang-1].Thang = thang;
                            quyBuResponse.QuyBuThangs[thang-1].PhatSinh = quyBuNam.data.FirstOrDefault(qb => qb.Thang == thang && maQuyBus.data.Any(mqb => mqb.MaQuyBu == qb.MaQuyBu))?.PhatSinh ?? 0;
                            quyBuResponse.QuyBuThangs[thang-1].SuDung = quyBuNam.data.FirstOrDefault(qb => qb.Thang == thang && maQuyBus.data.Any(mqb => mqb.MaQuyBu == qb.MaQuyBu))?.SuDung ?? 0;
                            phatSinh += quyBuResponse.QuyBuThangs[thang - 1].PhatSinh;
                            suDung += quyBuResponse.QuyBuThangs[thang - 1].SuDung;
                        }
                        conLai = phatSinh - suDung;
                    }
                    else
                    {
                        conLai = phatSinh;
                    }

                    quyBuResponse.SuDung = suDung;
                    quyBuResponse.ConLai = conLai;
                    quyBuResponse.PhatSinh = phatSinh;
                    listQuyBuResponse.Add(quyBuResponse);
                }
                return GetBaseResult(CodeMessage._200, data: listQuyBuResponse.AsEnumerable());
            }
            return GetBaseResult<IEnumerable<QuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }
    }
}
