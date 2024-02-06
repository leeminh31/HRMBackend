using AutoMapper;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.PhongBan;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.PhongBan.Response;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.PhongBan
{
    public class PhongBanService : BaseService, IPhongBanService
    {
        #region Property
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public PhongBanService(IPhongBanDAO phongBanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._phongBanDAO = phongBanDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<PhongBanResponse>> CreateAsync(CreatePhongBanRequest request)
        {
            // Mapping Resource to PhongBan
            var airport = Mapper.Map<CreatePhongBanRequest, Models.PhongBan>(request);
            SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _phongBanDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<PhongBanResponse>(records.data.First()));
            }

            var result = await _phongBanDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<PhongBanResponse>(result.data));
            else
                return GetBaseResult<PhongBanResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<PhongBanResponse>>> GetByCodeOrNameAsync(SearchPhongBanRequest request)
        {
            var records = await _phongBanDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<PhongBanResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<PhongBanResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<PhongBanResponse>>> GetAllPhongBanAsync()
        {
            var records = await _phongBanDAO.GetAllPhongBanAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<PhongBanResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<PhongBanResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<PhongBanResponse>>> PaginationGetByCodeAndNameAsync(PaginationPhongBanRequest request)
        {
            var resultDAO = await _phongBanDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<PhongBanResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<PhongBanResponse>>, IEnumerable<PhongBanResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<PhongBanResponse>>, IEnumerable<PhongBanResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        //public async Task<BaseResult<PhongBanResponse>> UpdateAsync(UpdatePhongBanRequest request)
        //{
        //    // Mapping Resource to PhongBan
        //    var airport = Mapper.Map<UpdatePhongBanRequest, Models.PhongBan>(request);
        //    SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { Code = request.Code, Name = request.Name };
        //    //Tìm mã code hoặc name đã tồn tại chưa?
        //    var records = await _phongBanDAO.GetByCodeOrNameAsync(searchRequest);
        //    if (records.isSuccess)
        //    {
        //        var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
        //        if (anyExist.Count > 0)
        //        {
        //            return GetBaseResult(CodeMessage._547, data: Mapper.Map<PhongBanResponse>(anyExist.FirstOrDefault()));
        //        }
        //    }

        //    var result = await _phongBanDAO.UpdateAsync(airport);
        //    await _unitOfWork.SaveChangesAsync();

        //    if (result.isSuccess)
        //        return GetBaseResult(CodeMessage._200, data: Mapper.Map<PhongBanResponse>(result.data));
        //    else
        //        return GetBaseResult<PhongBanResponse>(CodeMessage._236, status: StatusEnum.Failed);
        //}
    }
}
