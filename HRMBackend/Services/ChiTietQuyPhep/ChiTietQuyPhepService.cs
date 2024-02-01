using AutoMapper;
using HRMBackend.DataAccess.ChiTietQuyPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.ChiTietQuyPhep;
using Microsoft.Extensions.Options;
using HRMBackend.Extensions;

namespace HRMBackend.Services.ChiTietQuyPhep
{
    public class ChiTietQuyPhepService : BaseService, IChiTietQuyPhepService
    {
        #region Property
        private readonly IChiTietQuyPhepDAO _chiTietQuyPhepDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ChiTietQuyPhepService(IChiTietQuyPhepDAO chiTietQuyPhepDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyPhepDAO = chiTietQuyPhepDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<ChiTietQuyPhepResponse>> CreateAsync(CreateChiTietQuyPhepRequest request)
        {
            // Mapping Resource to ChiTietQuyPhep
            var airport = Mapper.Map<CreateChiTietQuyPhepRequest, Models.ChiTietQuyPhep>(request);
            SearchChiTietQuyPhepRequest searchRequest = new SearchChiTietQuyPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _chiTietQuyPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<ChiTietQuyPhepResponse>(records.data.First()));
            }

            var result = await _chiTietQuyPhepDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<ChiTietQuyPhepResponse>(result.data));
            else
                return GetBaseResult<ChiTietQuyPhepResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<ChiTietQuyPhepResponse>>> GetByCodeOrNameAsync(SearchChiTietQuyPhepRequest request)
        {
            var records = await _chiTietQuyPhepDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<ChiTietQuyPhepResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<ChiTietQuyPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<ChiTietQuyPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationChiTietQuyPhepRequest request)
        {
            var resultDAO = await _chiTietQuyPhepDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<ChiTietQuyPhepResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<ChiTietQuyPhepResponse>>, IEnumerable<ChiTietQuyPhepResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<ChiTietQuyPhepResponse>>, IEnumerable<ChiTietQuyPhepResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<ChiTietQuyPhepResponse>> UpdateAsync(UpdateChiTietQuyPhepRequest request)
        {
            // Mapping Resource to ChiTietQuyPhep
            var airport = Mapper.Map<UpdateChiTietQuyPhepRequest, Models.ChiTietQuyPhep>(request);
            SearchChiTietQuyPhepRequest searchRequest = new SearchChiTietQuyPhepRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _chiTietQuyPhepDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<ChiTietQuyPhepResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _chiTietQuyPhepDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<ChiTietQuyPhepResponse>(result.data));
            else
                return GetBaseResult<ChiTietQuyPhepResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
