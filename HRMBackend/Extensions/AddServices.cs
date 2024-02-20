using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.TaiKhoan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Mapping.TaiKhoan;
using HRMBackend.Resources;
using HRMBackend.Services.HopDong;
using HRMBackend.Services.NhanVien;
using HRMBackend.Services.PhongBan;
using HRMBackend.Services.TaiKhoan;
using HRMBackend.Services.TokenManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace HRMBackend.Extensions
{
    public static class AddServices
    {
        public static void AddDependencyInjection(this IServiceCollection services, ConfigurationManager configuration)
        {
            //services.AddScoped<ICaLamViecDAO, CaLamViecDAO>();
            //services.AddScoped<ICaLamViecService, CaLamViecService>();

            //services.AddScoped<IDangKyCaDAO, DangKyCaDAO>();
            //services.AddScoped<IDangKyCaService, DangKyCaService>();

            //services.AddScoped<IDonBuDAO, DonBuDAO>();
            //services.AddScoped<IDonBuService, DonBuService>();

            //services.AddScoped<IDonConNhoDAO, DonConNhoDAO>();
            //services.AddScoped<IDonConNhoService, DonConNhoService>();

            //services.AddScoped<IDonPhepDAO, DonPhepDAO>();
            //services.AddScoped<IDonPhepService, DonPhepService>();

            //services.AddScoped<IDonTangCaDAO, DonTangCaDAO>();
            //services.AddScoped<IDonTangCaService, DonTangCaService>();

            services.AddScoped<INhanVienDAO, NhanVienDAO>();
            services.AddScoped<INhanVienService, NhanVienService>();

            //services.AddScoped<IDuLieuChamCongDAO, DuLieuChamCongDAO>();
            //services.AddScoped<IDuLieuChamCongService, DuLieuChamCongService>();

            //services.AddScoped<IGiaiTrinhDAO, GiaiTrinhDAO>();
            //services.AddScoped<IGiaiTrinhService, GiaiTrinhService>();

            services.AddScoped<IHopDongDAO, HopDongDAO>();
            services.AddScoped<IHopDongService, HopDongService>();

            //services.AddScoped<IChiTietQuyBuDAO, ChiTietQuyBuDAO>();
            //services.AddScoped<IChiTietQuyBuService, ChiTietQuyBuService>();

            //services.AddScoped<IChiTietQuyPhepDAO, ChiTietQuyPhepDAO>();
            //services.AddScoped<IChiTietQuyPhepService, ChiTietQuyPhepService>();

            services.AddScoped<IPhongBanDAO, PhongBanDAO>();
            services.AddScoped<IPhongBanService, PhongBanService>();

            //services.AddScoped<IQuyBuDAO, QuyBuDAO>();
            //services.AddScoped<IQuyBuService, QuyBuService>();

            //services.AddScoped<IQuyPhepDAO, QuyPhepDAO>();
            //services.AddScoped<IQuyPhepService, QuyPhepService>();

            services.AddScoped<ITaiKhoanDAO, TaiKhoanDAO>();
            services.AddScoped<ITaiKhoanService, TaiKhoanService>();

            services.AddScoped<ITokenManagementService, TokenManagementService>();

            services.AddScoped<IUnitOfWorkContext>(x => new UnitOfWorkContext(Global.ConnectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(ResourceToModelProfile));

        }
        public static void AddCustomizeSwagger(this IServiceCollection services)
        {
            var title = "HRM System API";
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = $"v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = title,
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }
    }
}
