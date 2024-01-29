using AutoMapper;
using HRMBackend.DataAccess.GiaiTrinh;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.GiaiTrinh;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.GiaiTrinh.Response;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.GiaiTrinh
{
    public class GiaiTrinhService : BaseService, IGiaiTrinhService
    {
        #region Property
        private readonly IGiaiTrinhDAO _giaiTrinhDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public GiaiTrinhService(IGiaiTrinhDAO giaiTrinhDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._giaiTrinhDAO = giaiTrinhDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<GiaiTrinhResponse>> CreateAsync(CreateGiaiTrinhRequest request)
        {
            // Mapping Resource to GiaiTrinh
            var airport = Mapper.Map<CreateGiaiTrinhRequest, Models.GiaiTrinh>(request);
            SearchGiaiTrinhRequest searchRequest = new SearchGiaiTrinhRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _giaiTrinhDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<GiaiTrinhResponse>(records.data.First()));
            }

            var result = await _giaiTrinhDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<GiaiTrinhResponse>(result.data));
            else
                return GetBaseResult<GiaiTrinhResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<GiaiTrinhResponse>>> GetByCodeOrNameAsync(SearchGiaiTrinhRequest request)
        {
            var records = await _giaiTrinhDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<GiaiTrinhResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<GiaiTrinhResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<GiaiTrinhResponse>>> PaginationGetByCodeAndNameAsync(PaginationGiaiTrinhRequest request)
        {
            var resultDAO = await _giaiTrinhDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<GiaiTrinhResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<GiaiTrinhResponse>>, IEnumerable<GiaiTrinhResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<GiaiTrinhResponse>>, IEnumerable<GiaiTrinhResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<GiaiTrinhResponse>> UpdateAsync(UpdateGiaiTrinhRequest request)
        {
            // Mapping Resource to GiaiTrinh
            var airport = Mapper.Map<UpdateGiaiTrinhRequest, Models.GiaiTrinh>(request);
            SearchGiaiTrinhRequest searchRequest = new SearchGiaiTrinhRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _giaiTrinhDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<GiaiTrinhResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _giaiTrinhDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<GiaiTrinhResponse>(result.data));
            else
                return GetBaseResult<GiaiTrinhResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
