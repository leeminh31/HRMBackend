using AutoMapper;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DonPhep;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DonPhep
{
    public class DonPhepService : BaseService, IDonPhepService
    {
        #region Property
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonPhepService(IDonPhepDAO donPhepDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donPhepDAO = donPhepDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DonPhepResponse>> CreateAsync(CreateDonPhepRequest request)
        {
            // Mapping Resource to DonPhep
            var airport = Mapper.Map<CreateDonPhepRequest, Models.DonPhep>(request);
            SearchDonPhepRequest searchRequest = new SearchDonPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonPhepResponse>(records.data.First()));
            }

            var result = await _donPhepDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonPhepResponse>(result.data));
            else
                return GetBaseResult<DonPhepResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DonPhepResponse>>> GetByCodeOrNameAsync(SearchDonPhepRequest request)
        {
            var records = await _donPhepDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DonPhepResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DonPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DonPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonPhepRequest request)
        {
            var resultDAO = await _donPhepDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DonPhepResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DonPhepResponse>>, IEnumerable<DonPhepResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DonPhepResponse>>, IEnumerable<DonPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<DonPhepResponse>> UpdateAsync(UpdateDonPhepRequest request)
        {
            // Mapping Resource to DonPhep
            var airport = Mapper.Map<UpdateDonPhepRequest, Models.DonPhep>(request);
            SearchDonPhepRequest searchRequest = new SearchDonPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonPhepResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _donPhepDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonPhepResponse>(result.data));
            else
                return GetBaseResult<DonPhepResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
