using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.ChiTietQuyPhep;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.QuyPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Resources.DTO.QuyPhep.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Results;
using HRMBackend.Services.CaLamViec;
using Microsoft.Extensions.Options;

namespace HRMBackend.Services.QuyPhep
{
    public class QuyPhepService : BaseService, IQuyPhepService
    {
        #region Property
        private readonly IChiTietQuyPhepDAO _chiTietQuyPhepDAO;
        private readonly IQuyPhepDAO _quyPhepDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IHopDongDAO _hopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuyPhepService(IChiTietQuyPhepDAO chiTietQuyPhepDAO,
            IQuyPhepDAO quyPhepDAO,
            INhanVienDAO nhanVienDAO,
            IPhongBanDAO phongBanDAO,
            IHopDongDAO hopDongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyPhepDAO = chiTietQuyPhepDAO;
            this._quyPhepDAO= quyPhepDAO;
            this._nhanVienDAO=nhanVienDAO;
            this._phongBanDAO=phongBanDAO;
            this._hopDongDAO=hopDongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<QuyPhepResponse>>> GetByYearAsync(SearchQuyPhepRequest request)
        {
            var ngayDauTienNam = new DateTime(request.Nam, 1, 1);

            var ngayCuoiCungNam = new DateTime(request.Nam, 12, 31);

            var quyPhepNam = await _chiTietQuyPhepDAO.GetByYearAsync(request.Nam);

            var phongBanSearchRequest = new SearchPhongBanRequest();
            phongBanSearchRequest.TenPhongBan = request.TenPhongBan;

            var phongBan = await _phongBanDAO.GetByParamsAsync(phongBanSearchRequest);

            var nhanVienTheoTen = await _nhanVienDAO.GetByParamsAsync(request.MaNhanVien,null, null,null,request.TenNhanVien);


            var listQuyPhepResponse = new List<QuyPhepResponse>();

            if (nhanVienTheoTen.isSuccess && phongBan.hasValue)
            {
                foreach (var nhanVien in nhanVienTheoTen.data)
                {
                    var quyPhepResponse = new QuyPhepResponse();
                    quyPhepResponse.PhongBan = phongBan.data.FirstOrDefault(pb => pb.MaPhongBan == nhanVien.MaPhongBan)?.TenPhongBan;
                    if (quyPhepResponse.PhongBan == null) {
                        continue;
                    }
                    quyPhepResponse.MaNhanVien = nhanVien.MaNhanVien;
                    quyPhepResponse.Nam = request.Nam;
                    quyPhepResponse.HoTen = nhanVien.HoTen;

                    var hopDong = await _hopDongDAO.GetContractByYearAsync(nhanVien.MaNhanVien, request.Nam);
                    var tongQuyNam = 0;
                    var suDung = 0;
                    var conLai = 0;
                    if (hopDong.hasValue)
                    {
                        foreach(var hd in  hopDong.data)
                        {
                            if(hd.NgayBatDauHopDong <= ngayDauTienNam && hd.NgayKetThucHopDong >= ngayDauTienNam && hd.NgayKetThucHopDong <= ngayCuoiCungNam)
                            {
                                tongQuyNam = hd.NgayKetThucHopDong.Month;
                            }  
                            
                            if (hd.NgayBatDauHopDong >= ngayDauTienNam && hd.NgayKetThucHopDong <= ngayCuoiCungNam )
                            {
                                tongQuyNam = hd.NgayKetThucHopDong.Month - hd.NgayBatDauHopDong.Month + 1;
                            }

                            if (hd.NgayBatDauHopDong >= ngayDauTienNam && hd.NgayKetThucHopDong > ngayCuoiCungNam)
                            {
                                tongQuyNam = 13 - hd.NgayBatDauHopDong.Month ;
                            }

                            if (hd.NgayBatDauHopDong < ngayDauTienNam && hd.NgayKetThucHopDong > ngayCuoiCungNam)
                            {
                                tongQuyNam = 12;
                            }


                            var maQuyPheps = await _quyPhepDAO.GetByEmployeeIDAsync(nhanVien.MaNhanVien);
                            if (maQuyPheps.isSuccess)
                            {
                                quyPhepResponse.Thang1 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 1 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang2 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 2 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang3 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 3 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang4 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 4 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang5 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 5 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang6 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 6 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang7 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 7 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang8 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 8 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang9 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 9 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang10 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 10 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang11 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 11 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;
                                quyPhepResponse.Thang12 = quyPhepNam.data.FirstOrDefault(qp => qp.Thang == 12 && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;

                                for (int thang = 1; thang <= 12; thang++)
                                {
                                    // Lấy giá trị của tháng tương ứng và cộng vào tổng
                                    int suDungTrongThang = quyPhepNam.data
                                        .FirstOrDefault(qp => qp.Thang == thang && maQuyPheps.data.Any(mqp => mqp.MaQuyPhep == qp.MaQuyPhep))?.SuDung ?? 0;

                                    suDung += suDungTrongThang;
                                }

                                conLai = tongQuyNam - suDung;
                            } else
                            {
                                conLai = tongQuyNam;
                            }
                        }
                    }

                    quyPhepResponse.DaDung = suDung;
                    quyPhepResponse.ConLai = conLai;
                    quyPhepResponse.TongPhep = tongQuyNam;
                    listQuyPhepResponse.Add(quyPhepResponse);
                }
                return GetBaseResult(CodeMessage._200, data: listQuyPhepResponse.AsEnumerable());
            }
            return GetBaseResult<IEnumerable<QuyPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }
    }
}
