using AutoMapper;
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
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Resources.DTO.DonConNho.Response;

namespace HRMBackend.Services.DanhSachDon
{
    public class DanhSachDonService : BaseService, IDanhSachDonService
    {
        #region Property
        private readonly IDonBuDAO _donBuDAO;
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DanhSachDonService(IDonBuDAO donBuDAO,
            IDonPhepDAO donPhepDAO,
            IDonTangCaDAO donTangCaDAO,
            IDonConNhoDAO donConNhoDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donBuDAO = donBuDAO;
            this._donTangCaDAO = donTangCaDAO;
            this._donPhepDAO = donPhepDAO;
            this._donConNhoDAO = donConNhoDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<DanhSachDonResponse>> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var danhSachDonResponse = new DanhSachDonResponse();
            danhSachDonResponse.listDonBu = new List<DonBuResponse>();
            danhSachDonResponse.listDonPhep = new List<DonPhepResponse>();
            danhSachDonResponse.listDonTangCa = new List<DonTangCaResponse>();
            danhSachDonResponse.listDonConNho = new List<DonConNhoResponse>();

            var listDonBu = await _donBuDAO.GetByParamsAsync(request);
            if (listDonBu.isSuccess)
            {
                danhSachDonResponse.listDonBu.AddRange(Mapper.Map<IEnumerable<DonBuResponse>>(listDonBu.data).ToList());
            }

            var listDonPhep = await _donPhepDAO.GetByParamsAsync(request);
            if (listDonPhep.isSuccess)
            {
                danhSachDonResponse.listDonPhep.AddRange(Mapper.Map<IEnumerable<DonPhepResponse>>(listDonPhep.data).ToList());
            }

            var listDonConNho = await _donConNhoDAO.GetByParamsAsync(request);
            if (listDonConNho.isSuccess)
            {
                danhSachDonResponse.listDonConNho.AddRange(Mapper.Map<IEnumerable<DonConNhoResponse>>(listDonConNho.data).ToList());
            }

            var listDonTangCa = await _donTangCaDAO.GetByParamsAsync(request);
            if(listDonTangCa.isSuccess)
            {
                danhSachDonResponse.listDonTangCa.AddRange(Mapper.Map<IEnumerable<DonTangCaResponse>>(listDonTangCa.data).ToList());
            }

            if (!listDonBu.isSuccess && !listDonConNho.isSuccess && !listDonPhep.isSuccess && !listDonTangCa.isSuccess) { 
                return GetBaseResult<DanhSachDonResponse>(CodeMessage._545, status: StatusEnum.Failed);
            }
            
            return GetBaseResult(CodeMessage._200, data: danhSachDonResponse);

        }
    }
}
