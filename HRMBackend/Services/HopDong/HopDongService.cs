using AutoMapper;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources.DTO.HopDong.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.HopDong;
using Microsoft.Extensions.Options;

namespace HRMBackend.Services.HopDong
{
    public class HopDongService : BaseService, IHopDongService
    {
        #region Property
        private readonly IHopDongDAO _hopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HopDongService(IHopDongDAO hopDongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._hopDongDAO = hopDongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<HopDongResponse>> CreateAsync(CreateHopDongRequest request)
        {
            // Mapping Resource to HopDong
            var airport = Mapper.Map<CreateHopDongRequest, Models.HopDong>(request);
            //SearchHopDongRequest searchRequest = new SearchHopDongRequest() { Code = request.Code, Name = request.Name };
            ////Tìm mã code hoặc name đã tồn tại chưa?
            //var records = await _hopDongDAO.GetByCodeOrNameAsync(searchRequest);
            //if (records.isSuccess)
            //{
            //    return GetBaseResult(CodeMessage._547, data: Mapper.Map<HopDongResponse>(records.data.First()));
            //}

            var result = await _hopDongDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<HopDongResponse>(result.data));
            else
                return GetBaseResult<HopDongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        //public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetByCodeOrNameAsync(SearchHopDongRequest request)
        //{
        //    var records = await _hopDongDAO.GetByCodeOrNameAsync(request);
        //    if (records.isSuccess)
        //    {
        //        return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
        //    }
        //    return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        //}
        public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetByParamsAsync(string? tenHopDong, string? tenNhanVien, string? loaiHopDong)
        {
            var records = await _hopDongDAO.GetByParamsAsync(tenHopDong, tenNhanVien, loaiHopDong);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetAllContractAsync()
        {
            var records = await _hopDongDAO.GetAllContractAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetByIDAsync(string maHopDong)
        {
            var records = await _hopDongDAO.GetByIDAsync(maHopDong);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        //public async Task<PaginationResult<IEnumerable<HopDongResponse>>> PaginationGetByCodeAndNameAsync(PaginationHopDongRequest request)
        //{
        //    var resultDAO = await _hopDongDAO.PaginationAsync(request);

        //    if (resultDAO.isSuccess)
        //    {
        //        // Mapping
        //        var resource = Mapper.Map<IEnumerable<HopDongResponse>>(resultDAO.data);

        //        var result = GetPaginationResult<PaginationResult<IEnumerable<HopDongResponse>>, IEnumerable<HopDongResponse>>(CodeMessage._200, resource);

        //        // Using extension-method for pagination
        //        result.CreatePaginationResponse(request, resultDAO.totalRecords);

        //        return result;
        //    }
        //    else
        //    {
        //        return GetPaginationResult<PaginationResult<IEnumerable<HopDongResponse>>, IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        //    }
        //}

        public async Task<BaseResult<HopDongResponse>> UpdateAsync(UpdateHopDongRequest request)
        {
            // Mapping Resource to HopDong
            var airport = Mapper.Map<UpdateHopDongRequest, Models.HopDong>(request);

            var result = await _hopDongDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<HopDongResponse>(result.data));
            else
                return GetBaseResult<HopDongResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
