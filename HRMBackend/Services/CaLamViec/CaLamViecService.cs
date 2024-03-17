using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Resources.DTO.PhongBan.Response;
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

        public async Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByShiftIdAsync(int? maCaLamViec, string? tenCa)
        {
            var records = await _caLamViecDAO.GetByShiftIDAsync(maCaLamViec, tenCa);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<CaLamViecResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<CaLamViecResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<CaLamViecResponse>> CreateAsync(CreateCaLamViecRequest request)
        {
            // Mapping Resource to PhongBan
            var phongban = Mapper.Map<CreateCaLamViecRequest, Models.CaLamViec>(request);
            //SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { TenPhongBan = request.TenPhongBan, ThuKyPhongBan = null, TruongPhongBan = null };
            //Tìm tên ca đã tồn tại chưa?
            var records = await _caLamViecDAO.GetByShiftNameAsync(request.TenCa);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._557, data: Mapper.Map<CaLamViecResponse>(records.data));
            }

            var result = await _caLamViecDAO.CreateAsync(phongban);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<CaLamViecResponse>(result.data));
            else
                return GetBaseResult<CaLamViecResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<CaLamViecResponse>> UpdateAsync(UpdateCaLamViecRequest request)
        {
            // Mapping Resource to PhongBan
            var airport = Mapper.Map<UpdateCaLamViecRequest, Models.CaLamViec>(request);
            //SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { TenPhongBan = request.TenPhongBan, ThuKyPhongBan = null, TruongPhongBan = null };
            //Tìm tên phòng ban đã tồn tại chưa?
            //var records = await _caLamViecDAO.GetByTenPhongBanAsync(request.TenPhongBan, request.MaPhongBan);
            //if (records.hasValue)
            //{
            //    return GetBaseResult(CodeMessage._551, data: Mapper.Map<PhongBanResponse>(records.data));
            //}

            var result = await _caLamViecDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<CaLamViecResponse>(result.data));
            else
                return GetBaseResult<CaLamViecResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> DeleteAsync(string id)
        {
            var isUserRoleSuccess = await _caLamViecDAO.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();

            if (isUserRoleSuccess)
                return GetBaseResult<bool>(CodeMessage._200);
            else
                return GetBaseResult<bool>(CodeMessage._210, status: StatusEnum.Failed);
        }
    }
}
