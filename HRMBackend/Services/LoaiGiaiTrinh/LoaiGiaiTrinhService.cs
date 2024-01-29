using AutoMapper;
using HRMBackend.DataAccess.LoaiGiaiTrinh;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.LoaiGiaiTrinh;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Response;
using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.LoaiGiaiTrinh
{
    public class LoaiGiaiTrinhService : BaseService, ILoaiGiaiTrinhService
    {
        #region Property
        private readonly ILoaiGiaiTrinhDAO _loaiGiaiTrinhDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LoaiGiaiTrinhService(ILoaiGiaiTrinhDAO loaiGiaiTrinhDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._loaiGiaiTrinhDAO = loaiGiaiTrinhDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<LoaiGiaiTrinhResponse>> CreateAsync(CreateLoaiGiaiTrinhRequest request)
        {
            // Mapping Resource to LoaiGiaiTrinh
            var airport = Mapper.Map<CreateLoaiGiaiTrinhRequest, Models.LoaiGiaiTrinh>(request);
            SearchLoaiGiaiTrinhRequest searchRequest = new SearchLoaiGiaiTrinhRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _loaiGiaiTrinhDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<LoaiGiaiTrinhResponse>(records.data.First()));
            }

            var result = await _loaiGiaiTrinhDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<LoaiGiaiTrinhResponse>(result.data));
            else
                return GetBaseResult<LoaiGiaiTrinhResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<LoaiGiaiTrinhResponse>>> GetByCodeOrNameAsync(SearchLoaiGiaiTrinhRequest request)
        {
            var records = await _loaiGiaiTrinhDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<LoaiGiaiTrinhResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<LoaiGiaiTrinhResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<LoaiGiaiTrinhResponse>>> PaginationGetByCodeAndNameAsync(PaginationLoaiGiaiTrinhRequest request)
        {
            var resultDAO = await _loaiGiaiTrinhDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<LoaiGiaiTrinhResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<LoaiGiaiTrinhResponse>>, IEnumerable<LoaiGiaiTrinhResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<LoaiGiaiTrinhResponse>>, IEnumerable<LoaiGiaiTrinhResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<LoaiGiaiTrinhResponse>> UpdateAsync(UpdateLoaiGiaiTrinhRequest request)
        {
            // Mapping Resource to LoaiGiaiTrinh
            var airport = Mapper.Map<UpdateLoaiGiaiTrinhRequest, Models.LoaiGiaiTrinh>(request);
            SearchLoaiGiaiTrinhRequest searchRequest = new SearchLoaiGiaiTrinhRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _loaiGiaiTrinhDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<LoaiGiaiTrinhResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _loaiGiaiTrinhDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<LoaiGiaiTrinhResponse>(result.data));
            else
                return GetBaseResult<LoaiGiaiTrinhResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
