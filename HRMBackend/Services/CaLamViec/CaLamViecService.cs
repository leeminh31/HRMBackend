using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Extensions;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Results;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;

namespace HRMBackend.Services.CaLamViec
{
    public class CaLamViecService : BaseService, ICaLamViecService
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

        public async Task<BaseResult<CaLamViecResponse>> CreateAsync(CreateCaLamViecRequest request)
        {
            // Mapping Resource to CaLamViec
            var airport = Mapper.Map<CreateCaLamViecRequest, Models.CaLamViec>(request);
            SearchCaLamViecRequest searchRequest = new SearchCaLamViecRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<CaLamViecResponse>(records.data.First()));
            }

            var result = await _caLamViecDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<CaLamViecResponse>(result.data));
            else
                return GetBaseResult<CaLamViecResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByCodeOrNameAsync(SearchCaLamViecRequest request)
        {
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<CaLamViecResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<CaLamViecResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<CaLamViecResponse>>> PaginationGetByCodeAndNameAsync(PaginationCaLamViecRequest request)
        {
            var resultDAO = await _caLamViecDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<CaLamViecResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<CaLamViecResponse>>, IEnumerable<CaLamViecResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<CaLamViecResponse>>, IEnumerable<CaLamViecResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<CaLamViecResponse>> UpdateAsync(UpdateCaLamViecRequest request)
        {
            // Mapping Resource to CaLamViec
            var airport = Mapper.Map<UpdateCaLamViecRequest, Models.CaLamViec>(request);
            SearchCaLamViecRequest searchRequest = new SearchCaLamViecRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<CaLamViecResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _caLamViecDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<CaLamViecResponse>(result.data));
            else
                return GetBaseResult<CaLamViecResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
