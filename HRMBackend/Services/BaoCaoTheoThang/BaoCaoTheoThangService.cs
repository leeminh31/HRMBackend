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

namespace HRMBackend.Services.BaoCaoTheoThang
{
    public class BaoCaoTheoThangService : BaseService, IBaoCaoTheoThangService
    {
        #region Property
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly ICaLamViecDAO _caLamViecDAO;
        #endregion

        #region Constructor
        public BaoCaoTheoThangService(INhanVienDAO nhanVienDAO,
            IPhongBanDAO phongBanDAO,
            IDuLieuChamCongDAO duLieuChamCongDAO,
            ICaLamViecDAO caLamViecDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._unitOfWork = unitOfWork;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
            this._caLamViecDAO = caLamViecDAO;
            this._duLieuChamCongDAO = duLieuChamCongDAO;
        }
        #endregion

        //public async Task<BaseResult<DuLieuChamCongResponse>> CreateAsync(CreateDuLieuChamCongRequest request)
        //{
        //    // Mapping Resource to DuLieuChamCong
        //    var airport = Mapper.Map<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>(request);
        //    //SearchDuLieuChamCongRequest searchRequest = new SearchDuLieuChamCongRequest() { Code = request.Code, Name = request.Name };
        //    //Tìm mã code hoặc name đã tồn tại chưa?
        //    var records = await _duLieuChamCongDAO.GetByIDAsync(request.TenDuLieuChamCong);
        //    if (records.hasValue)
        //    {
        //        return GetBaseResult(CodeMessage._552, data: Mapper.Map<DuLieuChamCongResponse>(records.data));
        //    }

        //    var result = await _duLieuChamCongDAO.CreateAsync(airport);
        //    await _unitOfWork.SaveChangesAsync();

        //    if (result.isSuccess)
        //        return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(result.data));
        //    else
        //        return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        //}

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

            var calamviec = await _caLamViecDAO.GetByShiftIDAsync(shiftId, null);

            var listWork = workHour.data.ToList();

            var lastCheck = listWork.MaxBy(t => t.LanChamCong);

            var firstCheck = listWork.MinBy(t => t.LanChamCong);

            var giobatdaulam = calamviec.data.First().GioBatDauCa;

            var gioketthuclam = calamviec.data.First().GioKetThucCa;

            var giobatdaunghi = calamviec.data.First().GioBatDauNghi;

            var gioketthucnghi = calamviec.data.First().GioKetThucNghi;

            var firstCheckTime = firstCheck.GioChamCong;

            var lastCheckTime = lastCheck.GioChamCong;

            if (firstCheck?.GioChamCong < giobatdaulam)
            {
                 firstCheckTime= giobatdaulam;
            }

            if (lastCheck?.GioChamCong > gioketthuclam)
            {
                lastCheckTime = gioketthuclam;
            }

            var totalWorkHours = Math.Round((lastCheckTime - firstCheckTime).TotalMinutes);

            
            var gionghi = Math.Round((gioketthucnghi - giobatdaunghi).TotalMinutes);
            totalWorkHours = totalWorkHours - gionghi;          
            
            var listBaoCao = new List<BaoCaoTheoThangResponse>();

            if( listWork != null)
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
            return GetBaseResult<List<BaoCaoTheoThangResponse>>(CodeMessage._545, status: StatusEnum.Success);
        }

        private bool HasSpecialChars(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        private bool IsValidEmail(string email)
        {
            if (!MailAddress.TryCreate(email, out var mailAddress))
                return false;

            // And if you want to be more strict:
            var hostParts = mailAddress.Host.Split('.');
            if (hostParts.Length == 1)
                return false; // No dot.
            if (hostParts.Any(p => p == string.Empty))
                return false; // Double dot.
            if (hostParts[^1].Length < 2)
                return false; // TLD only one letter.

            if (mailAddress.User.Contains(' '))
                return false;
            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
                return false; // Double dot or dot at end of user part.

            return true;
        }
    }
}
