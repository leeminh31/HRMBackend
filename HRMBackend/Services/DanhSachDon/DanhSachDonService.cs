using AutoMapper;
using HRMBackend.DataAccess.DanhSachDon;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DanhSachDon;
using Microsoft.Extensions.Options;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.DonTangCa;

namespace HRMBackend.Services.DanhSachDon
{
    public class DanhSachDonService : BaseService, IDanhSachDonService
    {
        #region Property
        private readonly IDanhSachDonDAO _DanhSachDonDAO;
        private readonly IDonBuDAO _donBuDAO;
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DanhSachDonService(IDanhSachDonDAO DanhSachDonDAO,
            IDonBuDAO donBuDAO,
            IDonPhepDAO donPhepDAO,
            IDonTangCaDAO donTangCaDAO,
            IDonConNhoDAO donConNhoDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._DanhSachDonDAO = DanhSachDonDAO;
            this._donBuDAO = donBuDAO;
            this._donTangCaDAO = donTangCaDAO;
            this._donPhepDAO = donPhepDAO;
            this._donConNhoDAO = donConNhoDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<IEnumerable<DanhSachDonResponse>>> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var listDonBu = 

            //var records = await _DanhSachDonDAO.GetByShiftIDAsync(request);
            //if (records.isSuccess)
            //{
            //    return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DanhSachDonResponse>>(records.data));
            //}
            return GetBaseResult<IEnumerable<DanhSachDonResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<DanhSachDonResponse>> CreateAsync(CreateDanhSachDonRequest request)
        {
            // Mapping Resource to PhongBan
            var phongban = Mapper.Map<CreateDanhSachDonRequest, Models.DanhSachDon>(request);
            //SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { TenPhongBan = request.TenPhongBan, ThuKyPhongBan = null, TruongPhongBan = null };
            //Tìm tên ca đã tồn tại chưa?
            var records = await _DanhSachDonDAO.GetByShiftNameAsync(request.TenCa);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._557, data: Mapper.Map<DanhSachDonResponse>(records.data));
            }

            var result = await _DanhSachDonDAO.CreateAsync(phongban);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DanhSachDonResponse>(result.data));
            else
                return GetBaseResult<DanhSachDonResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<DanhSachDonResponse>> UpdateAsync(UpdateDanhSachDonRequest request)
        {
            // Mapping Resource to PhongBan
            var airport = Mapper.Map<UpdateDanhSachDonRequest, Models.DanhSachDon>(request);
            //SearchPhongBanRequest searchRequest = new SearchPhongBanRequest() { TenPhongBan = request.TenPhongBan, ThuKyPhongBan = null, TruongPhongBan = null };
            //Tìm tên phòng ban đã tồn tại chưa?
            //var records = await _DanhSachDonDAO.GetByTenPhongBanAsync(request.TenPhongBan, request.MaPhongBan);
            //if (records.hasValue)
            //{
            //    return GetBaseResult(CodeMessage._551, data: Mapper.Map<PhongBanResponse>(records.data));
            //}

            var result = await _DanhSachDonDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DanhSachDonResponse>(result.data));
            else
                return GetBaseResult<DanhSachDonResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> DeleteAsync(string id)
        {
            var isUserRoleSuccess = await _DanhSachDonDAO.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();

            if (isUserRoleSuccess)
                return GetBaseResult<bool>(CodeMessage._200);
            else
                return GetBaseResult<bool>(CodeMessage._210, status: StatusEnum.Failed);
        }
    }
}
