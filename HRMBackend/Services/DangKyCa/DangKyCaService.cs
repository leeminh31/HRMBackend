using AutoMapper;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.Resources.DTO.DangKyCa.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DangKyCa;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.CaLamViec.Response;

namespace HRMBackend.Services.DangKyCa
{
    public class DangKyCaService : BaseService, IDangKyCaService
    {
        #region Property
        private readonly IDangKyCaDAO _dangKyCaDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DangKyCaService(IDangKyCaDAO dangKyCaDAO,
            INhanVienDAO nhanVienDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._dangKyCaDAO = dangKyCaDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DangKyCaResponse>> CreateAsync(CreateDangKyCaRequest request)
        {
            // Mapping Resource to PhongBan
            var phongban = Mapper.Map<CreateDangKyCaRequest, Models.DangKyCa>(request);

            var result = await _dangKyCaDAO.CreateAsync(phongban);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DangKyCaResponse>(result.data));
            else
                return GetBaseResult<DangKyCaResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DangKyCaResponse>>> GetByParamsAsync(SearchDangKyCaRequest request)
        {
            var records = await _dangKyCaDAO.GetByParamsAsync(request);
            
            if (records.isSuccess)
            {
                var result = records.data;

                var nhanVienID = await _nhanVienDAO.GetAllEmployeeIdByNameAsync(request.TenNhanVien);

                if (request.TrangThai != null)
                {
                    result = result.Where(don => don.TrangThai == request.TrangThai);
                }

                if (nhanVienID.hasValue)
                {
                    result = result.Where(don => nhanVienID.data.Contains(don.MaNhanVien));
                } else
                {
                    return GetBaseResult<IEnumerable<DangKyCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
                }

                if(result.Count() > 0)
                {
                    return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DangKyCaResponse>>(result));
                }
            }
            return GetBaseResult<IEnumerable<DangKyCaResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> ApproveShiftRequestAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _dangKyCaDAO.ApproveShiftRequestAsync(maGiaiTrinh, nguoiDuyet);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }

        public async Task<BaseResult<bool>> RejectShiftRequestAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _dangKyCaDAO.RejectShiftRequestAsync(maGiaiTrinh, nguoiDuyet);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }
    }
}
