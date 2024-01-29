using AutoMapper;
using HRMBackend.DataAccess.QuyPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.QuyPhep;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.QuyPhep.Response;
using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.QuyPhep
{
    public class QuyPhepService : BaseService, IQuyPhepService
    {
        #region Property
        private readonly IQuyPhepDAO _quyPhepDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuyPhepService(IQuyPhepDAO quyPhepDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._quyPhepDAO = quyPhepDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<QuyPhepResponse>> CreateAsync(CreateQuyPhepRequest request)
        {
            // Mapping Resource to QuyPhep
            var airport = Mapper.Map<CreateQuyPhepRequest, Models.QuyPhep>(request);
            SearchQuyPhepRequest searchRequest = new SearchQuyPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _quyPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<QuyPhepResponse>(records.data.First()));
            }

            var result = await _quyPhepDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<QuyPhepResponse>(result.data));
            else
                return GetBaseResult<QuyPhepResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<QuyPhepResponse>>> GetByCodeOrNameAsync(SearchQuyPhepRequest request)
        {
            var records = await _quyPhepDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<QuyPhepResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<QuyPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<QuyPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationQuyPhepRequest request)
        {
            var resultDAO = await _quyPhepDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<QuyPhepResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<QuyPhepResponse>>, IEnumerable<QuyPhepResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<QuyPhepResponse>>, IEnumerable<QuyPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<QuyPhepResponse>> UpdateAsync(UpdateQuyPhepRequest request)
        {
            // Mapping Resource to QuyPhep
            var airport = Mapper.Map<UpdateQuyPhepRequest, Models.QuyPhep>(request);
            SearchQuyPhepRequest searchRequest = new SearchQuyPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _quyPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<QuyPhepResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _quyPhepDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<QuyPhepResponse>(result.data));
            else
                return GetBaseResult<QuyPhepResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
