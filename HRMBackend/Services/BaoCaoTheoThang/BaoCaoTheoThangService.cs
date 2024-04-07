using AutoMapper;
using ExcelDataReader;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DuLieuChamCong;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using HRMBackend.Services.PhanCaNhanVien;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Response;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Request;
using HRMBackend.DataAccess.CaLamViec;
using Org.BouncyCastle.Asn1.Ocsp;
using HRMBackend.DataAccess.DangKyCa;
using System.Collections.Generic;

namespace HRMBackend.Services.BaoCaoTheoThang
{
    public class BaoCaoTheoThangService : BaseService, IBaoCaoTheoThangService
    {
        #region Property
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IDangKyCaDAO _dangKyCaDAO;
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly ICaLamViecDAO _caLamViecDAO;
        #endregion

        #region Constructor
        public BaoCaoTheoThangService(INhanVienDAO nhanVienDAO,
            IDangKyCaDAO dangKyCaDAO,
            IDuLieuChamCongDAO duLieuChamCongDAO,
            ICaLamViecDAO caLamViecDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._unitOfWork = unitOfWork;
            this._nhanVienDAO = nhanVienDAO;
            this._dangKyCaDAO = dangKyCaDAO;
            this._caLamViecDAO = caLamViecDAO;
            this._duLieuChamCongDAO = duLieuChamCongDAO;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByParamsAsync(SearchBaoCaoTheoThangRequest request)
        {
            var timeKeeping = await _duLieuChamCongDAO.GetByParamsAsync(request.MaNhanVien, request.NgayBatDau, request.NgayKetThuc);
            var employeeData = await _nhanVienDAO.GetByParamsAsync(request.MaNhanVien, null, null, null, request.TenNhanVien);
            var filterRecords = timeKeeping.data?.Where(c => employeeData.data.Select(i => i.MaNhanVien).Contains(c.MaNhanVien));

            if (filterRecords!.GetEnumerator().MoveNext())
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(filterRecords));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Success);
        }

        public async Task<BaseResult<List<BaoCaoTheoThangResponse>>> GetTotalHourkWorkByDayAsync(string? maNhanVien, DateTime? ngayLamViec)
        {
            var workHour = await _duLieuChamCongDAO.GetTotalHourkWorkByDayAsync(maNhanVien, ngayLamViec);

            if( workHour.data == null)
                return GetBaseResult<List<BaoCaoTheoThangResponse>>(CodeMessage._545, status: StatusEnum.Success);

            var nhanVien = await _nhanVienDAO.GetByParamsAsync(maNhanVien, null, null, null, null);

            var shiftId = nhanVien.data.First().MaCa;

            var listWork = workHour.data.ToList();

            var lastCheck = listWork.MaxBy(t => t.LanChamCong);

            var firstCheck = listWork.MinBy(t => t.LanChamCong);

            var firstCheckTime = firstCheck.GioChamCong;

            var lastCheckTime = lastCheck.GioChamCong;

            if (shiftId != 0)
            {
                var calamviec = await _caLamViecDAO.GetByShiftIDAsync(shiftId, null);

                var giobatdaulam = calamviec.data.First().GioBatDauCa;

                var gioketthuclam = calamviec.data.First().GioKetThucCa;

                var giobatdaunghi = calamviec.data.First().GioBatDauNghi;

                var gioketthucnghi = calamviec.data.First().GioKetThucNghi;

                if (firstCheck?.GioChamCong < giobatdaulam)
                {
                    firstCheckTime = giobatdaulam;
                }

                if (lastCheck?.GioChamCong > gioketthuclam)
                {
                    lastCheckTime = gioketthuclam;
                }
                var gioNghi = Math.Round((gioketthucnghi - giobatdaunghi).TotalMinutes);

                var totalWorkHours = Math.Round((lastCheckTime - firstCheckTime).TotalMinutes);

                if (lastCheck?.GioChamCong >= gioketthucnghi)
                    totalWorkHours = totalWorkHours - gioNghi;

                if (lastCheck?.GioChamCong >= giobatdaunghi && lastCheck?.GioChamCong <= gioketthucnghi)
                {
                    totalWorkHours = Math.Round((giobatdaunghi - giobatdaulam).TotalMinutes);
                }

                var listBaoCao = new List<BaoCaoTheoThangResponse>();

                if (listWork != null)
                {
                    foreach (var item in listWork)
                    {
                        var baoCao = Mapper.Map<BaoCaoTheoThangResponse>(item);
                        baoCao.ThoiGianLamViecThucTe = totalWorkHours;
                        listBaoCao.Add(baoCao);
                    }
                }

                if (workHour.hasValue)
                {
                    return GetBaseResult(CodeMessage._200, data: listBaoCao);
                }
            }     
            
            return GetBaseResult<List<BaoCaoTheoThangResponse>>(CodeMessage._545, status: StatusEnum.Success);
        }

        public async Task<BaseResult<IEnumerable<Models.NhanVien>>> AssignShiftsToEmployeeAsync()
        {
            var phanCa = await _dangKyCaDAO.GetLatestShiftToUpdateAsync();

            if (phanCa.isSuccess)
            {
                var listEmployee = new List<Models.NhanVien>();
                var listEmployeeIdString = phanCa.data.Select(c => $"'{c.MaNhanVien}'");
                var employeeId = string.Join(", ", listEmployeeIdString);
                foreach (var item in phanCa.data)
                {
                    var caLamViec = await _caLamViecDAO.GetByShiftNameAsync(item.CaLamViecMoi);
                    var maCa = caLamViec.data.MaCa;
                    var employee = new Models.NhanVien();
                    employee.MaNhanVien = item.MaNhanVien;
                    employee.MaCa = maCa;
                    listEmployee.Add(employee);
                }
                var updateData = await _nhanVienDAO.UpdateShiftIDAsync(listEmployee.AsEnumerable(), employeeId);
                await _unitOfWork.SaveChangesAsync();
                if (updateData.isSuccess)
                {
                    return GetBaseResult<IEnumerable<Models.NhanVien>> (CodeMessage._200, data: updateData.data);
                }
            }

            return GetBaseResult<IEnumerable<Models.NhanVien>>(CodeMessage._558, status: StatusEnum.Success);
        }
    }
}
