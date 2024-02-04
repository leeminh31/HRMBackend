using AutoMapper;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DuLieuChamCong;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.DuLieuChamCong
{
    public class DuLieuChamCongService : BaseService, IDuLieuChamCongService
    {
        #region Property
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DuLieuChamCongService(IDuLieuChamCongDAO duLieuChamCongDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._duLieuChamCongDAO = duLieuChamCongDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DuLieuChamCongResponse>> CreateAsync(CreateDuLieuChamCongRequest request)
        {
            // Mapping Resource to DuLieuChamCong
            var airport = Mapper.Map<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>(request);
            SearchDuLieuChamCongRequest searchRequest = new SearchDuLieuChamCongRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _duLieuChamCongDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DuLieuChamCongResponse>(records.data.First()));
            }

            var result = await _duLieuChamCongDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(result.data));
            else
                return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByCodeOrNameAsync(SearchDuLieuChamCongRequest request)
        {
            var records = await _duLieuChamCongDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<DuLieuChamCongResponse>>> PaginationGetByCodeAndNameAsync(PaginationDuLieuChamCongRequest request)
        {
            var resultDAO = await _duLieuChamCongDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<DuLieuChamCongResponse>>, IEnumerable<DuLieuChamCongResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<DuLieuChamCongResponse>>, IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

    //    public async Task<BaseResult<DuLieuChamCongResponse>> UpdateAsync(UpdateDuLieuChamCongRequest request)
    //    {
    //        // Mapping Resource to DuLieuChamCong
    //        var airport = Mapper.Map<UpdateDuLieuChamCongRequest, Models.DuLieuChamCong>(request);
    //        SearchDuLieuChamCongRequest searchRequest = new SearchDuLieuChamCongRequest() { Code = request.Code, Name = request.Name };
    //        //Tìm mã code hoặc name đã tồn tại chưa?
    //        var records = await _duLieuChamCongDAO.GetByCodeOrNameAsync(searchRequest);
    //        if (records.isSuccess)
    //        {
    //            var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
    //            if (anyExist.Count > 0)
    //            {
    //                return GetBaseResult(CodeMessage._547, data: Mapper.Map<DuLieuChamCongResponse>(anyExist.FirstOrDefault()));
    //            }
    //        }

    //        var result = await _duLieuChamCongDAO.UpdateAsync(airport);
    //        await _unitOfWork.SaveChangesAsync();

    //        if (result.isSuccess)
    //            return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(result.data));
    //        else
    //            return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._236, status: StatusEnum.Failed);
    //    }
    //}
}
}
