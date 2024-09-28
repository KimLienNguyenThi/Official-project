using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Service_Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// đăng ký database
builder.Services.AddDbContext<QuanLyThuVienContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyThuVien")),
    ServiceLifetime.Transient);
// đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Đăng ký dịch vụ cho IMapper
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddTransient<ThongKeService, ThongKeService>();
builder.Services.AddTransient<PhieuTraService, PhieuTraService>();
builder.Services.AddTransient<QuanLyPhieuTraService, QuanLyPhieuTraService>();
builder.Services.AddTransient<QuanLyPhieuMuonService, QuanLyPhieuMuonService>();
builder.Services.AddTransient<PhieuMuonService, PhieuMuonService>();
builder.Services.AddTransient<KhoSachService, KhoSachService>();
builder.Services.AddTransient<NhapSachService, NhapSachService>();
builder.Services.AddTransient<ThanhLySachService, ThanhLySachService>();
builder.Services.AddTransient<TheDocGiaService, TheDocGiaService>();
builder.Services.AddTransient<ThongTinDocGiaService, ThongTinDocGiaService>();
builder.Services.AddTransient<AccountService, AccountService>();
builder.Services.AddTransient<DangKyMuonSachService, DangKyMuonSachService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
