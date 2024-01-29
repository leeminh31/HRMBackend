using AutoMapper;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.CaLamViec;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.Resources.DTO.DangKyCa.Response;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DangKyCa
{
    public class DangKyCaService : BaseService, IDangKyCaService
    {
        #region Property
        private readonly IDangKyCaDAO _dangKyCaDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DangKyCaService(IDangKyCaDAO dangKyCaDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._dangKyCaDAO = dangKyCaDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DangKyCaResponse>> CreateAsync(CreateDangKyCaRequest request)
        {
            // Mapping Resource to DangKyCa
            var airport = Mapper.Map<CreateDangKyCaRequest, Models.DangKyCa>(request);
            SearchDangKyCaRequest searchRequest = new SearchDangKyCaRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _dangKyCaDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DangKyCaResponse>(records.data.First()));
            }

            var result = await _dangKyCaDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DangKyCaResponse>(result.data));
            else
                return GetBaseResult<DangKyCaResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DangKyCaResponse>>> GetByCodeOrNameAsync(SearchDangKyCaRequest request)
        {
            var records = await _dangKyCaDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DangKyCaResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DangKyCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DangKyCaResponse>>> PaginationGetByCodeAndNameAsync(PaginationDangKyCaRequest request)
        {
            var resultDAO = await _dangKyCaDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DangKyCaResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DangKyCaResponse>>, IEnumerable<DangKyCaResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DangKyCaResponse>>, IEnumerable<DangKyCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<DangKyCaResponse>> UpdateAsync(UpdateDangKyCaRequest request)
        {
            // Mapping Resource to DangKyCa
            var airport = Mapper.Map<UpdateDangKyCaRequest, Models.DangKyCa>(request);
            SearchDangKyCaRequest searchRequest = new SearchDangKyCaRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _dangKyCaDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<DangKyCaResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _dangKyCaDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DangKyCaResponse>(result.data));
            else
                return GetBaseResult<DangKyCaResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
