using AutoMapper;
using HRMBackend.DataAccess.PhanQuyen;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.PhanQuyen;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.PhanQuyen.Response;
using HRMBackend.Resources.DTO.PhanQuyen.Request;
using HRMBackend.Extensions;

namespace HRMBackend.Services.PhanQuyen
{
    public class PhanQuyenService : BaseService, IPhanQuyenService
    {
        #region Property
        private readonly IPhanQuyenDAO _phanQuyenDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public PhanQuyenService(IPhanQuyenDAO phanQuyenDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._phanQuyenDAO = phanQuyenDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<PhanQuyenResponse>> CreateAsync(CreatePhanQuyenRequest request)
        {
            // Mapping Resource to PhanQuyen
            var airport = Mapper.Map<CreatePhanQuyenRequest, Models.PhanQuyen>(request);
            SearchPhanQuyenRequest searchRequest = new SearchPhanQuyenRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _phanQuyenDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._547, data: Mapper.Map<PhanQuyenResponse>(records.data.First()));
            }

            var result = await _phanQuyenDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<PhanQuyenResponse>(result.data));
            else
                return GetBaseResult<PhanQuyenResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<PhanQuyenResponse>>> GetByCodeOrNameAsync(SearchPhanQuyenRequest request)
        {
            var records = await _phanQuyenDAO.GetByCodeOrNameAsync(request);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<PhanQuyenResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<PhanQuyenResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<PaginationResult<IEnumerable<PhanQuyenResponse>>> PaginationGetByCodeAndNameAsync(PaginationPhanQuyenRequest request)
        {
            var resultDAO = await _phanQuyenDAO.PaginationAsync(request);

            if (resultDAO.isSuccess)
            {
                // Mapping
                var resource = Mapper.Map<IEnumerable<PhanQuyenResponse>>(resultDAO.data);

                var result = GetPaginationResult<PaginationResult<IEnumerable<PhanQuyenResponse>>, IEnumerable<PhanQuyenResponse>>(CodeMessage._200, resource);

                // Using extension-method for pagination
                result.CreatePaginationResponse(request, resultDAO.totalRecords);

                return result;
            }
            else
            {
                return GetPaginationResult<PaginationResult<IEnumerable<PhanQuyenResponse>>, IEnumerable<PhanQuyenResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
        }

        public async Task<BaseResult<PhanQuyenResponse>> UpdateAsync(UpdatePhanQuyenRequest request)
        {
            // Mapping Resource to PhanQuyen
            var airport = Mapper.Map<UpdatePhanQuyenRequest, Models.PhanQuyen>(request);
            SearchPhanQuyenRequest searchRequest = new SearchPhanQuyenRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _phanQuyenDAO.GetByCodeOrNameAsync(searchRequest);
            if (records.isSuccess)
            {
                var anyExist = records.data.Where(x => x.Id != request.Id).ToList();
                if (anyExist.Count > 0)
                {
                    return GetBaseResult(CodeMessage._547, data: Mapper.Map<PhanQuyenResponse>(anyExist.FirstOrDefault()));
                }
            }

            var result = await _phanQuyenDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<PhanQuyenResponse>(result.data));
            else
                return GetBaseResult<PhanQuyenResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }
    }
}
}
