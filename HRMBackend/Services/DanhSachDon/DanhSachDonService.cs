using AutoMapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Request;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.Services.QuyBu;
using HRMBackend.DataAccess.QuyBu;

namespace HRMBackend.Services.DanhSachDon
{
    public class DanhSachDonService : BaseService, IDanhSachDonService
    {
        #region Property
        private readonly ICaLamViecDAO _caLamViecDAO;
        private readonly IDonBuDAO _donBuDAO;
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly IHopDongDAO _hopDongDAO;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IQuyBuDAO _quyBuDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DanhSachDonService(IDonBuDAO donBuDAO,
            IDonPhepDAO donPhepDAO,
            IDonTangCaDAO donTangCaDAO,
            IDonConNhoDAO donConNhoDAO,
            IQuyBuDAO quyBuDAO,
            INhanVienDAO nhanVienDAO,
            IDuLieuChamCongDAO duLieuChamCongDAO,
            IHopDongDAO hopDongDAO,
            ICaLamViecDAO caLamViecDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donBuDAO = donBuDAO;
            this._donTangCaDAO = donTangCaDAO;
            this._hopDongDAO = hopDongDAO;
            this._caLamViecDAO = caLamViecDAO;
            this._quyBuDAO = quyBuDAO;
            this._duLieuChamCongDAO = duLieuChamCongDAO;
            this._donPhepDAO = donPhepDAO;
            this._donConNhoDAO = donConNhoDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DanhSachDonResponse>> GetByEmployeeIdAsync(string maNhanVien, DateTime ngayLamViec)
        {
            var request = new SearchDonByDayRequest() { 
                MaNhanVien = maNhanVien,
                NgayLamViec = ngayLamViec,
            };

            var danhSachDonResponse = new DanhSachDonResponse();
            danhSachDonResponse.listDonBu = new List<DonBuResponse>();
            danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
            danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
            danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();

            var listDonBu = await _donBuDAO.GetDonBuByDayAsync(request);
            var listDonPhep = await _donPhepDAO.GetDonPhepByDayAsync(request);
            var listDonConNho = await _donConNhoDAO.GetDonConNhoByDayAsync(request);
            var listDonTangCa = await _donTangCaDAO.GetDonTangCaByDayAsync(request);

            if (listDonBu.isSuccess)
            {
                danhSachDonResponse.listDonBu.AddRange(Mapper.Map<IEnumerable<DonBuResponse>>(listDonBu.data).ToList());
            }

            if (listDonPhep.isSuccess)
            {
                danhSachDonResponse.listDonPhep.AddRange(Mapper.Map<IEnumerable<DonPhepResponse>>(listDonPhep.data).ToList());
            }

            if (listDonConNho.isSuccess)
            {
                danhSachDonResponse.listDonConNho.AddRange(Mapper.Map<IEnumerable<DonConNhoResponse>>(listDonConNho.data).ToList());
            }

            if (listDonTangCa.isSuccess)
            {
                danhSachDonResponse.listDonTangCa.AddRange(Mapper.Map<IEnumerable<DonTangCaResponse>>(listDonTangCa.data).ToList());
            }

            if (!listDonBu.isSuccess && !listDonConNho.isSuccess && !listDonPhep.isSuccess && !listDonTangCa.isSuccess)
            {
                return GetBaseResult<DanhSachDonResponse>(CodeMessage._545, status: StatusEnum.Failed);
            }

            danhSachDonResponse.listDonPhep = danhSachDonResponse.listDonPhep.ToList();
            danhSachDonResponse.listDonBu = danhSachDonResponse.listDonBu.ToList();
            danhSachDonResponse.listDonConNho = danhSachDonResponse.listDonConNho.ToList();
            danhSachDonResponse.listDonTangCa = danhSachDonResponse.listDonTangCa.ToList();

            return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);

        }
        public async Task<BaseResult<DanhSachDonResponse>> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var danhSachDonResponse = new DanhSachDonResponse();
            danhSachDonResponse.listDonBu = new List<DonBuResponse>();
            danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
            danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
            danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();

            var listDonBu = await _donBuDAO.GetByParamsAsync(request);
            var listDonPhep = await _donPhepDAO.GetByParamsAsync(request);
            var listDonConNho = await _donConNhoDAO.GetByParamsAsync(request);
            var listDonTangCa = await _donTangCaDAO.GetByParamsAsync(request);

            if (listDonBu.isSuccess)
            {
                danhSachDonResponse.listDonBu.AddRange(Mapper.Map<IEnumerable<DonBuResponse>>(listDonBu.data).ToList());
            }
            
            if (listDonPhep.isSuccess)
            {
                danhSachDonResponse.listDonPhep.AddRange(Mapper.Map<IEnumerable<DonPhepResponse>>(listDonPhep.data).ToList());
            }

            if (listDonConNho.isSuccess)
            {
                danhSachDonResponse.listDonConNho.AddRange(Mapper.Map<IEnumerable<DonConNhoResponse>>(listDonConNho.data).ToList());
            }

            if(listDonTangCa.isSuccess)
            {
                danhSachDonResponse.listDonTangCa.AddRange(Mapper.Map<IEnumerable<DonTangCaResponse>>(listDonTangCa.data).ToList());
            }

            if (request.TrangThai != null)
            {
                danhSachDonResponse.listDonTangCa = danhSachDonResponse.listDonTangCa.Where(donTangCa => donTangCa.TrangThai == request.TrangThai).ToList();
                danhSachDonResponse.listDonBu = danhSachDonResponse.listDonBu.Where(donTangCa => donTangCa.TrangThai == request.TrangThai).ToList();
                danhSachDonResponse.listDonConNho = danhSachDonResponse.listDonConNho.Where(donTangCa => donTangCa.TrangThai == request.TrangThai).ToList();
                danhSachDonResponse.listDonPhep = danhSachDonResponse.listDonPhep.Where(donTangCa => donTangCa.TrangThai == request.TrangThai).ToList();
            }

            if (request.TenNhanVien != null)
            {
                var listEmployee = await _nhanVienDAO.GetAllEmployeeIdByNameAsync(request.TenNhanVien);
                if(listEmployee.hasValue)
                {
                    var listEmployeeId = listEmployee.data;
                    danhSachDonResponse.listDonPhep = danhSachDonResponse.listDonPhep.Where(donPhep => listEmployeeId.Contains(donPhep.MaNhanVien)).ToList();
                    danhSachDonResponse.listDonBu = danhSachDonResponse.listDonBu.Where(donPhep => listEmployeeId.Contains(donPhep.MaNhanVien)).ToList();
                    danhSachDonResponse.listDonConNho = danhSachDonResponse.listDonConNho.Where(donPhep => listEmployeeId.Contains(donPhep.MaNhanVien)).ToList();
                    danhSachDonResponse.listDonTangCa = danhSachDonResponse.listDonTangCa.Where(donPhep => listEmployeeId.Contains(donPhep.MaNhanVien)).ToList();
                } else
                {
                    danhSachDonResponse.listDonBu = new List<DonBuResponse>();
                    danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
                    danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
                    danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();
                    return GetBaseResult<DanhSachDonResponse>(CodeMessage._545, status: StatusEnum.Failed);
                }
            }

            if (request.LoaiDon != null)
            {
                if (request.LoaiDon == 1)
                {
                    danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();
                    danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
                    danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
                    return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);
                }

                if (request.LoaiDon == 2)
                {
                    danhSachDonResponse.listDonBu = new List<DonBuResponse>();
                    danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
                    danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
                    return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);
                }

                if (request.LoaiDon == 3)
                {
                    danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();
                    danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
                    danhSachDonResponse.listDonBu = new List<DonBuResponse>();
                    return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);
                }

                if (request.LoaiDon == 4)
                {
                    danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();
                    danhSachDonResponse.listDonBu = new List<DonBuResponse>();
                    danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
                    return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);
                }
            }

            if (!listDonBu.isSuccess && !listDonConNho.isSuccess && !listDonPhep.isSuccess && !listDonTangCa.isSuccess) { 
                return GetBaseResult<DanhSachDonResponse>(CodeMessage._545, status: StatusEnum.Failed);
            }
            
            return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);

        }

        public async Task<BaseResult<bool>> ApproveAllRequestAsync(ApproveRequestList request)
        {
            var result = await _donBuDAO.ApproveAllRequestAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }

        public async Task<BaseResult<bool>> RejectAllRequestAsync(ApproveRequestList request)
        {
            var result = await _donBuDAO.RejectAllRequestAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return GetBaseResult(CodeMessage._200, true);
        }

        public async Task<BaseResult<DonBuResponse>> CreateDonBuAsync(CreateDonBuRequest request)
        {
            var donbu = Mapper.Map<CreateDonBuRequest, Models.DonBu>(request);

            var searchDonBuByDay = new SearchDonByDayRequest();
            searchDonBuByDay.MaNhanVien = request.MaNhanVien;
            searchDonBuByDay.NgayLamViec = request.NgayLamViec;
            var checkDonBu = await _donBuDAO.GetDonBuByDayAsync(searchDonBuByDay);

            if(checkDonBu.isSuccess)
            {
                return GetBaseResult<DonBuResponse>(CodeMessage._209, status: StatusEnum.Failed);
            }

            var result = await _donBuDAO.CreateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonBuResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonBuResponse>(result.data));
        }

        public async Task<BaseResult<DonBuResponse>> UpdateDonBuAsync(UpdateDonBuRequest request)
        {
            var donbu = Mapper.Map<UpdateDonBuRequest, Models.DonBu>(request);
            var result = await _donBuDAO.UpdateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonBuResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonBuResponse>(result.data));
        }

        public async Task<BaseResult<DonConNhoResponse>> CreateDonConNhoAsync(CreateDonConNhoRequest request)
        {
            var donbu = Mapper.Map<CreateDonConNhoRequest, Models.DonConNho>(request);
            var searchDonConNhoRequest = new SearchDonByDayRequest();
            searchDonConNhoRequest.MaNhanVien = request.MaNhanVien;
            searchDonConNhoRequest.NgayLamViec = request.NgayLamViec;

            var checkDonConNho = await _donConNhoDAO.GetDonConNhoByDayAsync(searchDonConNhoRequest);

            if (checkDonConNho.isSuccess)
            {
                return GetBaseResult<DonConNhoResponse>(CodeMessage._209, status: StatusEnum.Failed);
            }

            var result = await _donConNhoDAO.CreateAsync(donbu);

            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonConNhoResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonConNhoResponse>(result.data));
        }

        public async Task<BaseResult<DonConNhoResponse>> UpdateDonConNhoAsync(UpdateDonConNhoRequest request)
        {
            var donbu = Mapper.Map<UpdateDonConNhoRequest, Models.DonConNho>(request);
            var result = await _donConNhoDAO.UpdateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonConNhoResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonConNhoResponse>(result.data));
        }

        public async Task<BaseResult<DonPhepResponse>> CreateDonPhepAsync(CreateDonPhepRequest request)
        {
            var donbu = Mapper.Map<CreateDonPhepRequest, Models.DonPhep>(request);
            var searchDonPhepRequest = new SearchDonByDayRequest();
            searchDonPhepRequest.MaNhanVien = request.MaNhanVien;
            searchDonPhepRequest.NgayLamViec = request.NgayLamViec;
            var checkDonPhep = await _donPhepDAO.GetDonPhepByDayAsync(searchDonPhepRequest);
            if (checkDonPhep.isSuccess)
            {
                return GetBaseResult<DonPhepResponse>(CodeMessage._209, status: StatusEnum.Failed);
            }

            var searchDuLieuChamCongRequest = new SearchBaoCaoTheoThangByDay();
            searchDuLieuChamCongRequest.MaNhanVien = request.MaNhanVien;
            searchDuLieuChamCongRequest.NgayLamViec = request.NgayLamViec;
            var checkDLCC = await _duLieuChamCongDAO.GetTotalHourkWorkByDayAsync(searchDuLieuChamCongRequest);
            if (checkDLCC.hasValue)
            {
                return GetBaseResult<DonPhepResponse>(CodeMessage._209, status: StatusEnum.Failed);
            }

            var result = await _donPhepDAO.CreateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonPhepResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonPhepResponse>(result.data));
        }

        public async Task<BaseResult<DonPhepResponse>> UpdateDonPhepAsync(UpdateDonPhepRequest request)
        {
            var donbu = Mapper.Map<UpdateDonPhepRequest, Models.DonPhep>(request);
            var result = await _donPhepDAO.UpdateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonPhepResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonPhepResponse>(result.data));
        }

        public async Task<BaseResult<DonTangCaResponse>> CreateDonTangCaAsync(CreateDonTangCaRequest request)
        {
            var donbu = Mapper.Map<CreateDonTangCaRequest, Models.DonTangCa>(request);
            var result = await _donTangCaDAO.CreateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonTangCaResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonTangCaResponse>(result.data));
        }

        public async Task<BaseResult<DonTangCaResponse>> UpdateDonTangCaAsync(UpdateDonTangCaRequest request)
        {
            var donbu = Mapper.Map<UpdateDonTangCaRequest, Models.DonTangCa>(request);
            var result = await _donTangCaDAO.UpdateAsync(donbu);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DonTangCaResponse>(result.data));
            }
            return GetBaseResult(CodeMessage._209, data: Mapper.Map<DonTangCaResponse>(result.data));
        }

        public async Task<BaseResult<int>> GetTotalMinutesOTAsync(string maNhanVien, int nam)
        {
            var records = await _quyBuDAO.GetByYearForEmployeeAsync(maNhanVien, nam);

            if (records.isSuccess)
            {
                var quyHienCo = 0;
                foreach (var record in records.data)
                {
                    quyHienCo = record.PhatSinh - record.SuDung;
                }
                return GetBaseResult(CodeMessage._200, data: quyHienCo);
            }
            return GetBaseResult<int>(CodeMessage._200, data: 0);
        }

        public async Task<BaseResult<double>> GetTotalDayOffByYearAsync(string maNhanVien, int nam)
        {
            double tongQuyPhep = 0;

            var hopdong = await _hopDongDAO.GetContractByYearAsync(maNhanVien, nam);

            var donPhep = await _donPhepDAO.GetDayOffByYearAndIDAsync(maNhanVien, nam);

            var maCa = await _nhanVienDAO.GetByIDAsync(maNhanVien);

            var caLamViec = await _caLamViecDAO.GetByShiftIDAsync(maCa.data.MaCa, null);

            var giobatdaulam = caLamViec.data.First().GioBatDauCa;

            var gioketthuclam = caLamViec.data.First().GioKetThucCa;

            var giobatdaunghi = caLamViec.data.First().GioBatDauNghi;

            var gioketthucnghi = caLamViec.data.First().GioKetThucNghi;

            var workHourByDay = Math.Round((gioketthuclam - giobatdaulam - gioketthucnghi + giobatdaunghi).TotalMinutes);

            if (hopdong.hasValue)
            {
                foreach(Models.HopDong hd in hopdong.data)
                {
                    if(hd.NgayBatDauHopDong.Year != hd.NgayKetThucHopDong.Year)
                    {
                        tongQuyPhep += 12 - hd.NgayBatDauHopDong.Month; 
                    } else
                    {
                        tongQuyPhep += hd.NgayKetThucHopDong.Month - hd.NgayBatDauHopDong.Month;
                    }
                }

                if(donPhep.isSuccess)
                {
                    tongQuyPhep -= donPhep.data;
                }
                return GetBaseResult<double>(CodeMessage._200, data: tongQuyPhep*workHourByDay);
            }
            return GetBaseResult<double>(CodeMessage._200, data: 0);
        }
    }
}
