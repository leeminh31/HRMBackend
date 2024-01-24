using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
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

        public async Task<BaseResult<AirportResponse>> CreateAsync(CreateAirportRequest request)
        {
            // Mapping Resource to Airport
            var airport = Mapper.Map<CreateAirportRequest, Models.Airport>(request);
            SearchAirportRequest searchRequest = new SearchAirportRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<AirportResponse>(records.data.First()));
            }

            var result = await _caLamViecDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<AirportResponse>(result.data));
            else
                return GetBaseResult<AirportResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<AirportResponse>>> GetByCodeOrNameAsync(SearchAirportRequest request)
        {
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<AirportResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<AirportResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<AirportResponse>>> PaginationGetByCodeAndNameAsync(PaginationAirportRequest request)
        {
            var resultDAO = await _caLamViecDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<AirportResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<AirportResponse>>, IEnumerable<AirportResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<AirportResponse>>, IEnumerable<AirportResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<AirportResponse>> UpdateAsync(UpdateAirportRequest request)
        {
            // Mapping Resource to Airport
            var airport = Mapper.Map<UpdateAirportRequest, Models.Airport>(request);
            SearchAirportRequest searchRequest = new SearchAirportRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _caLamViecDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<AirportResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _caLamViecDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<AirportResponse>(result.data));
            else
                return GetBaseResult<AirportResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
