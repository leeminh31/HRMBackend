using HRMBackend.DataAccess;
using HRMBackend.Extensions;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.Authentication;

var builder = WebApplication.CreateBuilder(args);
// Gán giá trị cho Global
Global.ConnectionString = builder.Configuration.GetConnectionString("HRMDBConn");
builder.Services.AddHttpContextAccessor();

// Gán giá trị cho phần JwtConfig
builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
builder.Services.AddControllers();
builder.Services.AddResponseCaching();
builder.Services.AddJwtBearerAuthentication();
builder.Services.AddCustomizeSwagger();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

ConfigDapper.Mapping();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
