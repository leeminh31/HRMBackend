using AutoMapper;
using HRMBackend.DataAccess.QuyBu;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.QuyBu;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.QuyBu.Response;
using HRMBackend.Resources.DTO.QuyBu.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.QuyBu
{
    public class QuyBuService : BaseService, IQuyBuService
    {
        #region Property
        private readonly IQuyBuDAO _quyBuDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuyBuService(IQuyBuDAO quyBuDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._quyBuDAO = quyBuDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<QuyBuResponse>> CreateAsync(CreateQuyBuRequest request)
        {
            // Mapping Resource to QuyBu
            var airport = Mapper.Map<CreateQuyBuRequest, Models.QuyBu>(request);
            SearchQuyBuRequest searchRequest = new SearchQuyBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _quyBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<QuyBuResponse>(records.data.First()));
            }

            var result = await _quyBuDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<QuyBuResponse>(result.data));
            else
                return GetBaseResult<QuyBuResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<QuyBuResponse>>> GetByCodeOrNameAsync(SearchQuyBuRequest request)
        {
            var records = await _quyBuDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<QuyBuResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<QuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<QuyBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationQuyBuRequest request)
        {
            var resultDAO = await _quyBuDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<QuyBuResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<QuyBuResponse>>, IEnumerable<QuyBuResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<QuyBuResponse>>, IEnumerable<QuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<QuyBuResponse>> UpdateAsync(UpdateQuyBuRequest request)
        {
            // Mapping Resource to QuyBu
            var airport = Mapper.Map<UpdateQuyBuRequest, Models.QuyBu>(request);
            SearchQuyBuRequest searchRequest = new SearchQuyBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _quyBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<QuyBuResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _quyBuDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<QuyBuResponse>(result.data));
            else
                return GetBaseResult<QuyBuResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
