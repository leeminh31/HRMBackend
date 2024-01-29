using AutoMapper;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DonTangCa;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DonTangCa
{
    public class DonTangCaService : BaseService, IDonTangCaService
    {
        #region Property
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonTangCaService(IDonTangCaDAO donTangCaDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donTangCaDAO = donTangCaDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DonTangCaResponse>> CreateAsync(CreateDonTangCaRequest request)
        {
            // Mapping Resource to DonTangCa
            var airport = Mapper.Map<CreateDonTangCaRequest, Models.DonTangCa>(request);
            SearchDonTangCaRequest searchRequest = new SearchDonTangCaRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donTangCaDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonTangCaResponse>(records.data.First()));
            }

            var result = await _donTangCaDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonTangCaResponse>(result.data));
            else
                return GetBaseResult<DonTangCaResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DonTangCaResponse>>> GetByCodeOrNameAsync(SearchDonTangCaRequest request)
        {
            var records = await _donTangCaDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DonTangCaResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DonTangCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DonTangCaResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonTangCaRequest request)
        {
            var resultDAO = await _donTangCaDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DonTangCaResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DonTangCaResponse>>, IEnumerable<DonTangCaResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DonTangCaResponse>>, IEnumerable<DonTangCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<DonTangCaResponse>> UpdateAsync(UpdateDonTangCaRequest request)
        {
            // Mapping Resource to DonTangCa
            var airport = Mapper.Map<UpdateDonTangCaRequest, Models.DonTangCa>(request);
            SearchDonTangCaRequest searchRequest = new SearchDonTangCaRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _donTangCaDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<DonTangCaResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _donTangCaDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonTangCaResponse>(result.data));
            else
                return GetBaseResult<DonTangCaResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
