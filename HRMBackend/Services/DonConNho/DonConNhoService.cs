using AutoMapper;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DonConNho;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DonConNho
{
    public class DonConNhoService : BaseService, IDonConNhoService
    {
        #region Property
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonConNhoService(IDonConNhoDAO donConNhoDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donConNhoDAO = donConNhoDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DonConNhoResponse>> CreateAsync(CreateDonConNhoRequest request)
        {
            // Mapping Resource to DonConNho
            var airport = Mapper.Map<CreateDonConNhoRequest, Models.DonConNho>(request);
            SearchDonConNhoRequest searchRequest = new SearchDonConNhoRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donConNhoDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonConNhoResponse>(records.data.First()));
            }

            var result = await _donConNhoDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonConNhoResponse>(result.data));
            else
                return GetBaseResult<DonConNhoResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DonConNhoResponse>>> GetByCodeOrNameAsync(SearchDonConNhoRequest request)
        {
            var records = await _donConNhoDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DonConNhoResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DonConNhoResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DonConNhoResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonConNhoRequest request)
        {
            var resultDAO = await _donConNhoDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DonConNhoResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DonConNhoResponse>>, IEnumerable<DonConNhoResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DonConNhoResponse>>, IEnumerable<DonConNhoResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<DonConNhoResponse>> UpdateAsync(UpdateDonConNhoRequest request)
        {
            // Mapping Resource to DonConNho
            var airport = Mapper.Map<UpdateDonConNhoRequest, Models.DonConNho>(request);
            SearchDonConNhoRequest searchRequest = new SearchDonConNhoRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donConNhoDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonConNhoResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _donConNhoDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonConNhoResponse>(result.data));
            else
                return GetBaseResult<DonConNhoResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
