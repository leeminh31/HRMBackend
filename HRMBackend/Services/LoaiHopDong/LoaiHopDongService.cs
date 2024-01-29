using AutoMapper;
using HRMBackend.DataAccess.LoaiHopDong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.LoaiHopDong;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.LoaiHopDong.Response;
using HRMBackend.Resources.DTO.LoaiHopDong.Request;

namespace HRMBackend.Services.LoaiHopDong
{
    public class LoaiHopDongService : BaseService, ILoaiHopDongService
    {
        #region Property
        private readonly ILoaiHopDongDAO _loaiHopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LoaiHopDongService(ILoaiHopDongDAO loaiHopDongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._loaiHopDongDAO = loaiHopDongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<LoaiHopDongResponse>> CreateAsync(CreateLoaiHopDongRequest request)
        {
            // Mapping Resource to LoaiHopDong
            var airport = Mapper.Map<CreateLoaiHopDongRequest, Models.LoaiHopDong>(request);
            SearchLoaiHopDongRequest searchRequest = new SearchLoaiHopDongRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _loaiHopDongDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<LoaiHopDongResponse>(records.data.First()));
            }

            var result = await _loaiHopDongDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<LoaiHopDongResponse>(result.data));
            else
                return GetBaseResult<LoaiHopDongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<LoaiHopDongResponse>>> GetByCodeOrNameAsync(SearchLoaiHopDongRequest request)
        {
            var records = await _loaiHopDongDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<LoaiHopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<LoaiHopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<LoaiHopDongResponse>>> PaginationGetByCodeAndNameAsync(PaginationLoaiHopDongRequest request)
        {
            var resultDAO = await _loaiHopDongDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<LoaiHopDongResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<LoaiHopDongResponse>>, IEnumerable<LoaiHopDongResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<LoaiHopDongResponse>>, IEnumerable<LoaiHopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<LoaiHopDongResponse>> UpdateAsync(UpdateLoaiHopDongRequest request)
        {
            // Mapping Resource to LoaiHopDong
            var airport = Mapper.Map<UpdateLoaiHopDongRequest, Models.LoaiHopDong>(request);
            SearchLoaiHopDongRequest searchRequest = new SearchLoaiHopDongRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _loaiHopDongDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<LoaiHopDongResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _loaiHopDongDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<LoaiHopDongResponse>(result.data));
            else
                return GetBaseResult<LoaiHopDongResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
