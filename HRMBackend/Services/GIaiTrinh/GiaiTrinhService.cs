using AutoMapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.UnitOfWork;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.GiaiTrinh;
using HRMBackend.Services.DanhSachDon;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Response;
using System.Collections;
using HRMBackend.Resources.DTO.DonBu.Request;

namespace HRMBackend.Services.GIaiTrinh
{
    public class GiaiTrinhService : BaseService, IGiaiTrinhService
    {
        #region Property
        private readonly IGiaiTrinhDAO _giaiTrinhDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public GiaiTrinhService(
            IGiaiTrinhDAO giaiTrinhDAO,
            INhanVienDAO nhanVienDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._giaiTrinhDAO = giaiTrinhDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion
        public async Task<BaseResult<IEnumerable<GiaiTrinhResponse>>> GetByEmployeeIdAsync(string? maNhanVien)
        {
            var request = new SearchGiaiTrinhRequest()
            {
                NgayLamViecBatDau = null,
                NgayLamViecKetThuc = null,
                NgayTaoBatDau = null,
                NgayTaoKetThuc = null,
                TenNhanVien = null,
                TrangThai = null,
                LoaiGiaiTrinh = null,
            };

            var listGiaiTrinh = await _giaiTrinhDAO.GetByParamsAsync(request);

            if (listGiaiTrinh.isSuccess)
            {
                listGiaiTrinh.data = listGiaiTrinh.data.Where(donPhep => maNhanVien == donPhep.MaNhanVien);
            }

            return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<GiaiTrinhResponse>>(listGiaiTrinh.data));

        }
        public async Task<BaseResult<IEnumerable<Models.GiaiTrinh>>> GetByParamsAsync (SearchGiaiTrinhRequest request)
        {
            var danhSachGiaiTrinhResponse = await _giaiTrinhDAO.GetByParamsAsync (request);

            if (danhSachGiaiTrinhResponse.isSuccess)
            {
                if (request.TrangThai != null)
                {
                    danhSachGiaiTrinhResponse.data = danhSachGiaiTrinhResponse.data.Where(giaiTrinh => giaiTrinh.TrangThai == request.TrangThai);
                    if (!danhSachGiaiTrinhResponse.data.GetEnumerator().MoveNext())
                    {
                        return GetBaseResult<IEnumerable<Models.GiaiTrinh>>(CodeMessage._545, status: StatusEnum.Failed);
                    }
                }

                if (request.TenNhanVien != null)
                {
                    var listEmployee = await _nhanVienDAO.GetAllEmployeeIdByNameAsync(request.TenNhanVien);
                    if (listEmployee.hasValue)
                    {
                        var listEmployeeId = listEmployee.data;
                        danhSachGiaiTrinhResponse.data = danhSachGiaiTrinhResponse.data.Where(giaiTrinh => listEmployeeId.Contains(giaiTrinh.MaNhanVien));
                    }
                    else
                    {
                        return GetBaseResult<IEnumerable<Models.GiaiTrinh>>(CodeMessage._545, status: StatusEnum.Failed);
                    }
                }
                return GetBaseResult(CodeMessage._200, data: danhSachGiaiTrinhResponse.data);
            }

            return GetBaseResult<IEnumerable<Models.GiaiTrinh>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> ApproveAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _giaiTrinhDAO.ApproveAllExplanationAsync(maGiaiTrinh, nguoiDuyet);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }

        public async Task<BaseResult<bool>> RejectAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _giaiTrinhDAO.RejectAllExplanationAsync(maGiaiTrinh, nguoiDuyet);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }

        public async Task<BaseResult<GiaiTrinhResponse>> CreateAsync(CreateGiaiTrinhRequest request)
        {
            var donbu = Mapper.Map<CreateGiaiTrinhRequest, Models.GiaiTrinh>(request);
            var result = await _giaiTrinhDAO.CreateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if ( result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<GiaiTrinhResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<GiaiTrinhResponse>(result.data));
        }
    }
}
