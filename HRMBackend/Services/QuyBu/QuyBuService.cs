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

namespace HRMBackend.Services.QuyBu
{
    public class QuyBuService : BaseService, IQuyBuService
    {
        #region Property
        private readonly IChiTietQuyBuDAO _chiTietQuyBuDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IHopDongDAO _hopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuyBuService(IChiTietQuyBuDAO chiTietQuyBuDAO,
            INhanVienDAO nhanVienDAO,
            IPhongBanDAO phongBanDAO,
            IHopDongDAO hopDongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyBuDAO = chiTietQuyBuDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
            this._hopDongDAO = hopDongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<QuyBuResponse>>> GetByYearAsync(int? nam)
        {
            var quyBuNam = await _chiTietQuyBuDAO.GetByYearAsync(nam);

            if (quyBuNam.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<QuyBuResponse>>(quyBuNam.data));
            }
            return GetBaseResult<IEnumerable<QuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }
    }
}
