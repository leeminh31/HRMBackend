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
            var airport = Mapper.Map<CreateTaiKhoanRequest, Models.TaiKhoan>(request);
            SearchTaiKhoanRequest searchRequest = new SearchTaiKhoanRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _taiKhoanDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<TaiKhoanResponse>(records.data.First()));
            }

            var result = await _taiKhoanDAO.CreateAsync(airport);
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

        public async Task<PaginationResult<IEnumerable<TaiKhoanResponse>>> PaginationGetByCodeAndNameAsync(PaginationTaiKhoanRequest request)
        {
            var resultDAO = await _taiKhoanDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<TaiKhoanResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<TaiKhoanResponse>>, IEnumerable<TaiKhoanResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<TaiKhoanResponse>>, IEnumerable<TaiKhoanResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<TaiKhoanResponse>> UpdateAsync(UpdateTaiKhoanRequest request)
        {
            // Mapping Resource to TaiKhoan
            var airport = Mapper.Map<UpdateTaiKhoanRequest, Models.TaiKhoan>(request);
            SearchTaiKhoanRequest searchRequest = new SearchTaiKhoanRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _taiKhoanDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<TaiKhoanResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _taiKhoanDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<TaiKhoanResponse>(result.data));
            else
                return GetBaseResult<TaiKhoanResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
