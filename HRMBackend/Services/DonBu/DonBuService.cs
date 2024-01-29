using AutoMapper;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DangKyCa;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DonBu
{
    public class DonBuService : BaseService, IDonBuService
    {
        #region Property
        private readonly IDonBuDAO _donBuDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonBuService(IDonBuDAO donBuDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donBuDAO = donBuDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DonBuResponse>> CreateAsync(CreateDonBuRequest request)
        {
            // Mapping Resource to DonBu
            var airport = Mapper.Map<CreateDonBuRequest, Models.DonBu>(request);
            SearchDonBuRequest searchRequest = new SearchDonBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonBuResponse>(records.data.First()));
            }

            var result = await _donBuDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonBuResponse>(result.data));
            else
                return GetBaseResult<DonBuResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DonBuResponse>>> GetByCodeOrNameAsync(SearchDonBuRequest request)
        {
            var records = await _donBuDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DonBuResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DonBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DonBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonBuRequest request)
        {
            var resultDAO = await _donBuDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DonBuResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DonBuResponse>>, IEnumerable<DonBuResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DonBuResponse>>, IEnumerable<DonBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<DonBuResponse>> UpdateAsync(UpdateDonBuRequest request)
        {
            // Mapping Resource to DonBu
            var airport = Mapper.Map<UpdateDonBuRequest, Models.DonBu>(request);
            SearchDonBuRequest searchRequest = new SearchDonBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonBuResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _donBuDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonBuResponse>(result.data));
            else
                return GetBaseResult<DonBuResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
