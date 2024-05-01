using AutoMapper;
using HRMBackend.DataAccess.TaiKhoan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.TaiKhoan;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.TaiKhoan.Response;
using HRMBackend.Resources.DTO.TaiKhoan.Request;
using HRMBackend.Extensions;
using HRMBackend.Resources.DTO.NhanVien.Response;

namespace HRMBackend.Services.TaiKhoan
{
    public class TaiKhoanService : BaseService, ITaiKhoanService
    {
        #region Property
        private readonly ITaiKhoanDAO _taiKhoanDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public TaiKhoanService(ITaiKhoanDAO taiKhoanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._taiKhoanDAO = taiKhoanDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<TaiKhoanResponse>> CreateAsync(CreateTaiKhoanRequest request)
        {
            // Mapping Resource to TaiKhoan
            var taiKhoan = Mapper.Map<CreateTaiKhoanRequest, Models.TaiKhoan>(request);

            var result = await _taiKhoanDAO.CreateAsync(taiKhoan);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<TaiKhoanResponse>(result.data));
            else
                return GetBaseResult<TaiKhoanResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<TaiKhoanResponse>>> GetByCodeOrNameAsync(SearchTaiKhoanRequest request)
        {
            var records = await _taiKhoanDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<TaiKhoanResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<TaiKhoanResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<string>>> GetEmployeeIdAsync()
        {
            var records = await _taiKhoanDAO.GetEmployeeIdAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: records.data);
            }
            return GetBaseResult<IEnumerable<string>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<TaiKhoanResponse>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            // Mapping Resource to TaiKhoan
            var taikhoan = Mapper.Map<ChangePasswordRequest, Models.TaiKhoan>(request);

            var result = await _taiKhoanDAO.ChangePasswordAsync(taikhoan);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<TaiKhoanResponse>(result.data));
            else
                return GetBaseResult<TaiKhoanResponse>(CodeMessage._560, status: StatusEnum.Failed);
        }
    }
}
