using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Results;
using Microsoft.Extensions.Options;

namespace HRMBackend.Services.CaLamViec
{
    public class CaLamViecService :BaseService, ICaLamViecService
    {
        #region Property
        private readonly ICaLamViecDAO _caLamViecDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CaLamViecService(ICaLamViecDAO caLamViecDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._caLamViecDAO = caLamViecDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByShiftIdAsync(int? maCaLamViec)
        {
            var records = await _caLamViecDAO.GetByShiftIDAsync(maCaLamViec);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<CaLamViecResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<CaLamViecResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }
    }
}
