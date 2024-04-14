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
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Response;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.Models;
using System.Net.WebSockets;
using HRMBackend.Resources.DTO.PhongBan.Request;

namespace HRMBackend.Services.DuLieuChamCong
{
    public class DuLieuChamCongService : BaseService, IDuLieuChamCongService
    {
        #region Property
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly ICaLamViecDAO _caLamViecDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IDonBuDAO _donBuDAO;
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IDangKyCaDAO _dangKyCaDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IHopDongDAO _hopDongDAO;
        #endregion

        #region Constructor
        public DuLieuChamCongService(IDuLieuChamCongDAO duLieuChamCongDAO,
            INhanVienDAO nhanVienDAO,
            ICaLamViecDAO caLamViecDAO,
            IDonBuDAO donBuDAO,
            IDangKyCaDAO dangKyCaDAO,
            IDonConNhoDAO donConNhoDAO,
            IDonPhepDAO donPhepDAO,
            IDonTangCaDAO donTangCaDAO,
            IHopDongDAO hopDongDAO,
            IPhongBanDAO phongBanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._duLieuChamCongDAO = duLieuChamCongDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._caLamViecDAO = caLamViecDAO;
            this._unitOfWork = unitOfWork;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
            this._donBuDAO = donBuDAO;
            this._dangKyCaDAO = dangKyCaDAO;
            this._donConNhoDAO = donConNhoDAO;
            this._hopDongDAO = hopDongDAO;
            this._donTangCaDAO = donTangCaDAO;
            this._donPhepDAO = donPhepDAO;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByParamsAsync(SearchDuLieuChamCongRequest request)
        {
            
            var records = await _duLieuChamCongDAO.GetByParamsAsync(request.MaNhanVien, request.NgayBatDau, request.NgayKetThuc);
            
            if(!records.isSuccess)
            {
                return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }

            var employeeData = await _nhanVienDAO.GetByParamsAsync(null, null, request.IDVanTay, null, request.TenNhanVien);

            if( !employeeData.isSuccess )
            {
                return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
            var filterRecords =  records.data?.Where(c => employeeData.data.Select(i => i.MaNhanVien).Contains(c.MaNhanVien));

            if (filterRecords!.GetEnumerator().MoveNext())
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(filterRecords));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>> GetByEmployeePerMonthAsync(SearchDuLieuChamCongByMonthRequest request)
        {
            // Lấy ngày đầu tiên của tháng
            DateTime firstDayOfMonth = new DateTime(request.NgayBatDau.Year, request.NgayBatDau.Month, 1);
            request.NgayBatDau = firstDayOfMonth;

            // Lấy ngày cuối cùng của tháng
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            request.NgayKetThuc = lastDayOfMonth;
            // Danh sách dữ liệu chấm theo tháng
            var records = await _duLieuChamCongDAO.GetByParamsAsync(null, request.NgayBatDau, request.NgayKetThuc);

            // Danh sách đơn phép theo tháng
            var searchRequest = new SearchDonPhepRequest();
            searchRequest.MaNhanVien = request?.MaNhanVien;
            searchRequest.NgayBatDauTaoDon = request?.NgayBatDau;
            searchRequest.NgayKetThucTaoDon = request?.NgayKetThuc;
            var donPhep = await _donPhepDAO.GetDayOffAsync(searchRequest);

            // Danh sách đơn con nhỏ theo tháng
            var searchDonConNhoRequest = new SearchDanhSachDonRequest();
            searchDonConNhoRequest.NgayLamViecBatDau = request?.NgayBatDau;
            searchDonConNhoRequest.NgayLamViecKetThuc = request?.NgayKetThuc;
            var donConNho = await _donConNhoDAO.GetDonConNhoByMonthAsync(searchDonConNhoRequest);

            //Danh sách đăng ký ca ảnh hưởng trong thời gian
            var dangKyCa = await _dangKyCaDAO.GetByEmployeeIDAsync(null, request.NgayBatDau, request.NgayKetThuc);

            // Danh sách tất cả phòng ban
            var listPhongBan = await _phongBanDAO.GetByParamsAsync(new SearchPhongBanRequest());

            // Danh sách nhân viên theo thông tin tìm kiếm tên nhân viên và mã nhân viên
            var listEmployee = await _nhanVienDAO.GetByParamsAsync(request.MaNhanVien, null, null, null, request.TenNhanVien);

            // Không có nhân viên phù hợp trả về rỗng 
            if (!listEmployee.isSuccess)
                return GetBaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>(CodeMessage._545, status: StatusEnum.Failed);

            // Danh sách mã phòng ban theo thông tin nhân viên 
            var phongBanEmployee = listEmployee.data.DistinctBy(e => e.MaPhongBan).Select(c=>c.MaPhongBan);

            // Danh sách thông tin phòng ban 
            var phongBanFilter = listPhongBan.data.Where(pb => phongBanEmployee.Any(pbe => pbe == pb.MaPhongBan));

            // Danh sách mã nhân viên
            var listEmployeeId = listEmployee.data.Select(e => e.MaNhanVien);

            // Lấy ra danh sách hợp đồng theo tháng của toàn bộ nhân viên
            var hopDongMonth = await _hopDongDAO.GetContractByTimeRangeAndIDAsync(request.NgayBatDau, request.NgayKetThuc);

            // Lấy ra danh sách ca làm việc của hệ thống
            var caLamviec = await _caLamViecDAO.GetByShiftIDAsync(null, null);

            var daysInMonth = (int)(request.NgayKetThuc - request.NgayBatDau).TotalDays + 1;

            //var recordsToList = records.data.ToList();
            //var recordsByEmployee = recordsToList.DistinctBy(item => item.MaNhanVien).ToList();

            var baoCaoTheoThangAll = new List<BaoCaoTheoThangAllResponse>();

            //Trả về dữ liệu chung cho từng nhân viên trong tháng
            foreach (var employee in listEmployee.data)
            {
                double totalWorkMonth = 0;
                var baoCaoTheoThangNhanVien = new BaoCaoTheoThangAllResponse();
                baoCaoTheoThangNhanVien.MaNhanVien = employee.MaNhanVien;
                baoCaoTheoThangNhanVien.IdVanTay = employee.IDVanTay;
                baoCaoTheoThangNhanVien.HoTen = employee.HoTen;
                baoCaoTheoThangNhanVien.Phong = phongBanFilter.FirstOrDefault(phong => phong.MaPhongBan == employee.MaPhongBan).TenPhongBan;
                baoCaoTheoThangNhanVien.DuLieuChamCongResponses = new List<DuLieuChamCongByDayResponse>();

                if (!hopDongMonth.hasValue || !hopDongMonth.data.Any(hd => hd.MaNhanVien == employee.MaNhanVien))
                {
                    baoCaoTheoThangNhanVien.TongCong = 0;
                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        var duLieuResponseDay = new DuLieuChamCongByDayResponse();
                        duLieuResponseDay.NgayLamViec = i;
                        duLieuResponseDay.IsYellow = false;

                        if (records.isSuccess)
                        {
                            var duLieuChamCongDay = records.data.Where(e => e.NgayChamCong == new DateTime((int)request.NgayKetThuc.Year, (int)request.NgayKetThuc.Month, i) && e.MaNhanVien == employee.MaNhanVien);
                            if (duLieuChamCongDay.GetEnumerator().MoveNext())
                            {
                                duLieuResponseDay.IsYellow = true;
                            }
                        }
                        baoCaoTheoThangNhanVien.DuLieuChamCongResponses.Add(duLieuResponseDay);
                    }
                }

                if (hopDongMonth.hasValue && hopDongMonth.data.Any(hd => hd.MaNhanVien == employee.MaNhanVien))
                {
                    var hopDongNhanVien = hopDongMonth.data.Where(hdm => hdm.MaNhanVien == employee.MaNhanVien);
                    foreach (var hopDong in hopDongNhanVien)
                    {
                        for (int i = 1; i <= daysInMonth; i++)
                        {
                            var ngayLamViec = new DateTime((int)request.NgayKetThuc.Year, (int)request.NgayKetThuc.Month, i);
                            var duLieuResponseDay = new DuLieuChamCongByDayResponse();
                            duLieuResponseDay.NgayLamViec = i;
                            duLieuResponseDay.IsYellow = false;
                            duLieuResponseDay.ConNho = false;
                            duLieuResponseDay.NghiPhep = false;
                            duLieuResponseDay.GioLamViec = 0;

                            // Logic đơn đăng ký ca của nhân viên
                            if (dangKyCa.isSuccess && dangKyCa.data.Any(dkc => dkc.MaNhanVien == employee.MaNhanVien))
                            {
                                var dangKyCaNhanVien = dangKyCa.data.Where(dkc => dkc.MaNhanVien == employee.MaNhanVien);
                                var ngayBatDauCaMoiGanNhat = DateTime.MinValue;
                                foreach (var dangKy in dangKyCaNhanVien)
                                {
                                    if ( ngayLamViec < ngayBatDauCaMoiGanNhat)
                                    {
                                        continue;
                                    }
                                    if (hopDong.NgayBatDauHopDong < request.NgayBatDau && hopDong.NgayKetThucHopDong < request.NgayKetThuc && hopDong.NgayKetThucHopDong >= request.NgayBatDau)
                                    {
                                        if (dangKy.NgayBatDauCaMoi <= request.NgayBatDau && ngayLamViec <= hopDong.NgayKetThucHopDong)
                                        {
                                            duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                        }

                                        if (dangKy.NgayBatDauCaMoi > request.NgayBatDau && ngayLamViec >= dangKy.NgayBatDauCaMoi && ngayLamViec <= hopDong.NgayKetThucHopDong)
                                        {
                                            duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                        }
                                        else if (ngayLamViec < dangKy.NgayBatDauCaMoi && ngayLamViec > ngayBatDauCaMoiGanNhat)
                                        {
                                            duLieuResponseDay.TenCa = dangKy.CaLamViecHienTai;
                                        }
                                    }

                                    if (hopDong.NgayBatDauHopDong > request.NgayBatDau && hopDong.NgayKetThucHopDong < request.NgayKetThuc)
                                    {
                                        if (dangKy.NgayBatDauCaMoi < hopDong.NgayBatDauHopDong)
                                        {
                                            if (ngayLamViec >= hopDong.NgayBatDauHopDong && ngayLamViec <= hopDong.NgayKetThucHopDong)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                            }
                                        }

                                        if (dangKy.NgayBatDauCaMoi >= hopDong.NgayBatDauHopDong)
                                        {
                                            if (ngayLamViec <= hopDong.NgayKetThucHopDong && ngayLamViec >= dangKy.NgayBatDauCaMoi)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                            }

                                            if (ngayLamViec < dangKy.NgayBatDauCaMoi && ngayLamViec >= hopDong.NgayBatDauHopDong && ngayLamViec > ngayBatDauCaMoiGanNhat)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecHienTai;
                                            }
                                        }
                                    }

                                    if (hopDong.NgayBatDauHopDong <= request.NgayBatDau && hopDong.NgayKetThucHopDong >= request.NgayKetThuc)
                                    {
                                        if (dangKy.NgayBatDauCaMoi <= request.NgayBatDau)
                                        {
                                            duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                        }
                                        else
                                        {
                                            if (ngayLamViec >= dangKy.NgayBatDauCaMoi)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                            }
                                            else if (ngayLamViec > ngayBatDauCaMoiGanNhat)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecHienTai;
                                            }
                                        }
                                    }

                                    if (hopDong.NgayBatDauHopDong <= request.NgayKetThuc && hopDong.NgayBatDauHopDong > request.NgayBatDau && hopDong.NgayKetThucHopDong > request.NgayKetThuc)
                                    {
                                        if (dangKy.NgayBatDauCaMoi >= hopDong.NgayBatDauHopDong)
                                        {
                                            if (ngayLamViec >= dangKy.NgayBatDauCaMoi && ngayLamViec <= request.NgayKetThuc)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                            }

                                            if (ngayLamViec >= hopDong.NgayBatDauHopDong && ngayLamViec < dangKy.NgayBatDauCaMoi && ngayLamViec > ngayBatDauCaMoiGanNhat)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecHienTai;
                                            }
                                        }
                                        else
                                        {
                                            if (ngayLamViec >= hopDong.NgayBatDauHopDong && ngayLamViec <= request.NgayKetThuc)
                                            {
                                                duLieuResponseDay.TenCa = dangKy.CaLamViecMoi;
                                            }
                                        }
                                    }
                                    ngayBatDauCaMoiGanNhat = dangKy.NgayBatDauCaMoi;
                                }
                            }
                            else
                            {
                                duLieuResponseDay.TenCa = "8A";
                            }

                            var thongTinCaNhanVien = caLamviec.data.FirstOrDefault(c => c.TenCa == duLieuResponseDay.TenCa);
                            var gioLamViecTheoCa = (double)Math.Round((thongTinCaNhanVien.GioKetThucCa - thongTinCaNhanVien.GioBatDauCa + thongTinCaNhanVien.GioBatDauNghi - thongTinCaNhanVien.GioKetThucNghi).TotalMinutes);
                            duLieuResponseDay.GioLamViecTheoCa = gioLamViecTheoCa;

                            // Logic đơn phép của nhân viên
                            if (donPhep.isSuccess)
                            {
                                var donPhepNhanVien = donPhep.data.FirstOrDefault(dp => dp.MaNhanVien == employee.MaNhanVien && dp.NgayLamViec == ngayLamViec);
                                if (donPhepNhanVien != null)
                                {
                                    duLieuResponseDay.NghiPhep = true;
                                }
                            }

                            // Giờ làm việc của nhân viên 
                            if (records.isSuccess)
                            {
                                var employeeByDay = records.data.Where(dlcc => dlcc.MaNhanVien == employee.MaNhanVien && dlcc.NgayChamCong == ngayLamViec);
                                if (employeeByDay.GetEnumerator().MoveNext())
                                {
                                    var lastCheck = employeeByDay.MaxBy(t => t.LanChamCong);

                                    var firstCheck = employeeByDay.MinBy(t => t.LanChamCong);

                                    var firstCheckTime = firstCheck.GioChamCong;

                                    var lastCheckTime = lastCheck.GioChamCong;

                                    if (firstCheckTime < thongTinCaNhanVien.GioBatDauCa)
                                    {
                                        firstCheckTime = thongTinCaNhanVien.GioBatDauCa;
                                    }

                                    if (lastCheck?.GioChamCong > thongTinCaNhanVien.GioKetThucCa)
                                    {
                                        lastCheckTime = thongTinCaNhanVien.GioKetThucCa;
                                    }

                                    var totalWorkHours = Math.Round((lastCheckTime - firstCheckTime).TotalMinutes);
                                    var gioNghi = (int)(thongTinCaNhanVien.GioKetThucNghi - thongTinCaNhanVien.GioBatDauNghi).TotalMinutes;

                                    if (lastCheck?.GioChamCong >= thongTinCaNhanVien.GioKetThucNghi)
                                        totalWorkHours = totalWorkHours - gioNghi;

                                    if (lastCheck?.GioChamCong >= thongTinCaNhanVien.GioBatDauNghi && lastCheck?.GioChamCong <= thongTinCaNhanVien.GioKetThucNghi)
                                    {
                                        totalWorkHours = Math.Round((thongTinCaNhanVien.GioBatDauNghi - thongTinCaNhanVien.GioBatDauCa).TotalMinutes);
                                    }
                                    duLieuResponseDay.GioLamViec = totalWorkHours;
                                    var totalWork = Math.Round(totalWorkHours / duLieuResponseDay.GioLamViecTheoCa, 2);
                                    totalWorkMonth += totalWork;
                                }
                            }
                            baoCaoTheoThangNhanVien.DuLieuChamCongResponses.Add(duLieuResponseDay);
                        }
                    }

                    baoCaoTheoThangNhanVien.TongCong = totalWorkMonth;

                }
                baoCaoTheoThangAll.Add(baoCaoTheoThangNhanVien);
            }
            return GetBaseResult(CodeMessage._200, data: baoCaoTheoThangAll.AsEnumerable());
        }

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetAllContractAsync()
        {
            var records = await _duLieuChamCongDAO.GetAllContractAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<DuLieuChamCongResponse>> GetByIDAsync(string maDuLieuChamCong)
        {
            var records = await _duLieuChamCongDAO.GetByIDAsync(maDuLieuChamCong);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(records.data));
            }
            return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> UploadFileTimeKeepingAsync(IFormFile file)
        {
            var listTimeKeepingRecords = new List<Models.DuLieuChamCong>();

            if (file != null && file.Length > 0)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var extension = Path.GetExtension(file.FileName);
                if (extension != ".xls" && extension != ".xlsx")
                    return GetBaseResult<bool>(CodeMessage._555, status: StatusEnum.Failed);

                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Upload\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            int isHeaderSkipped = 0;

                            while (reader.Read())
                            {
                                if (isHeaderSkipped < 1)
                                {
                                    isHeaderSkipped++;
                                    continue;
                                }

                                if (String.IsNullOrEmpty(reader.GetValue(1)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(2)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(3)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(4)?.ToString()))
                                {
                                    continue;
                                }

                                if ( !int.TryParse(reader.GetValue(3)?.ToString(), out int a)
                                    || !DateTime.TryParse(reader.GetValue(2)?.ToString(), out DateTime b)
                                    || !DateTime.TryParse(reader.GetValue(4)?.ToString(), out DateTime c))
                                {
                                    continue;
                                }

                                var listEmployeeId = await _nhanVienDAO.GetByIdVanTayAsync(int.Parse(reader.GetValue(1)?.ToString()), null);

                                CreateDuLieuChamCongRequest duLieuChamCong = new CreateDuLieuChamCongRequest();
                                duLieuChamCong.MaNhanVien = listEmployeeId.data.MaNhanVien;
                                duLieuChamCong.LanChamCong = int.Parse(reader.GetValue(3)?.ToString());
                                duLieuChamCong.NgayChamCong = DateTime.Parse(reader.GetValue(2)?.ToString());
                                duLieuChamCong.GioChamCong = DateTime.Parse(reader.GetValue(4)?.ToString()).TimeOfDay;

                                var duLieuChamCongModel = Mapper.Map<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>(duLieuChamCong);
                                listTimeKeepingRecords.Add(duLieuChamCongModel);
                                
                            }
                        } while (reader.NextResult());

                    }
                }

                System.IO.File.Delete(filePath);
            }

            var result = await _duLieuChamCongDAO.UpdateOrInsertListRecordsAsync(listTimeKeepingRecords.AsEnumerable());
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: true);
            else
                return GetBaseResult<bool>(CodeMessage._556, status: StatusEnum.Failed);
        }
    }
}
