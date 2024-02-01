using AutoMapper;
using HRMBackend.DataAccess.ChiTietQuyBu;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.ChiTietQuyBu.Request;
using HRMBackend.Resources.DTO.ChiTietQuyBu.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.ChiTietQuyBu;
using Microsoft.Extensions.Options;
using HRMBackend.Extensions;

namespace HRMBackend.Services.ChiTietQuyBu
{
    public class ChiTietQuyBuService : BaseService, IChiTietQuyBuService
    {
        #region Property
        private readonly IChiTietQuyBuDAO _chiTietQuyBuDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ChiTietQuyBuService(IChiTietQuyBuDAO chiTietQuyBuDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyBuDAO = chiTietQuyBuDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<ChiTietQuyBuResponse>> CreateAsync(CreateChiTietQuyBuRequest request)
        {
            // Mapping Resource to ChiTietQuyBu
            var airport = Mapper.Map<CreateChiTietQuyBuRequest, Models.ChiTietQuyBu>(request);
            SearchChiTietQuyBuRequest searchRequest = new SearchChiTietQuyBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _chiTietQuyBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<ChiTietQuyBuResponse>(records.data.First()));
            }

            var result = await _chiTietQuyBuDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<ChiTietQuyBuResponse>(result.data));
            else
                return GetBaseResult<ChiTietQuyBuResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<ChiTietQuyBuResponse>>> GetByCodeOrNameAsync(SearchChiTietQuyBuRequest request)
        {
            var records = await _chiTietQuyBuDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<ChiTietQuyBuResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<ChiTietQuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<ChiTietQuyBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationChiTietQuyBuRequest request)
        {
            var resultDAO = await _chiTietQuyBuDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<ChiTietQuyBuResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<ChiTietQuyBuResponse>>, IEnumerable<ChiTietQuyBuResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<ChiTietQuyBuResponse>>, IEnumerable<ChiTietQuyBuResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<ChiTietQuyBuResponse>> UpdateAsync(UpdateChiTietQuyBuRequest request)
        {
            // Mapping Resource to ChiTietQuyBu
            var airport = Mapper.Map<UpdateChiTietQuyBuRequest, Models.ChiTietQuyBu>(request);
            SearchChiTietQuyBuRequest searchRequest = new SearchChiTietQuyBuRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _chiTietQuyBuDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<ChiTietQuyBuResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _chiTietQuyBuDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<ChiTietQuyBuResponse>(result.data));
            else
                return GetBaseResult<ChiTietQuyBuResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
