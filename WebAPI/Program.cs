using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.Helper;
using WebAPI.Models;
using WebAPI.Service;
using WebAPI.Service_Admin;
using WebAPI.Services;
using WebAPI.Services.Admin;
using WebAPI.Services.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký dịch vụ Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// đăng ký database
builder.Services.AddDbContext<QuanLyThuVienContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyThuVien")),
    ServiceLifetime.Transient);
// đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// đăng ký dịch vụ Quest PDF
builder.Services.AddSingleton<GeneratePDFService, GeneratePDFService>();

// đăng ký dịch vụ JWT
builder.Services.AddAuthentication(option =>
{
    // Đặt mặc định schema xác thực là JwtBearer.Điều này đảm bảo mọi yêu cầu sẽ sử dụng JWT để xác thực.
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // Đặt mặc định schema thách thức là JwtBearer. Điều này được sử dụng khi xác thực thất bại(ví dụ: token không hợp lệ hoặc không được cung cấp).Hệ thống sẽ thách thức client bằng cách trả về mã 401 Unauthorized.
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // Đặt mặc định schema chính, áp dụng cho cả xác thực và thách thức.
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    // Cho phép API hoạt động qua HTTP(không yêu cầu HTTPS).Điều này hữu ích khi phát triển hoặc debug, nhưng bạn nên bật HTTPS trong môi trường sản xuất.
    option.RequireHttpsMetadata = false;
    // Lưu token đã xác thực vào HttpContext. Điều này có thể hữu ích nếu bạn cần sử dụng lại token trong quá trình xử lý.
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
    option.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                // context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        },
    };
});

builder.Services.AddAuthorization(options =>
{
    // Chính sách cho Admin
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    // Chính sách cho User
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Đăng ký dịch vụ cho IMapper
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<BangQuyDinhService>();

builder.Services.AddTransient<BookService, BookService>();
builder.Services.AddTransient<BorrowBookService, BorrowBookService>();
builder.Services.AddTransient<UserAuthService, UserAuthService>();
builder.Services.AddTransient<PhieuMuonService, PhieuMuonService>();
builder.Services.AddTransient<ThongKeService, ThongKeService>();
builder.Services.AddTransient<PhieuTraService, PhieuTraService>();
builder.Services.AddTransient<QuanLyPhieuTraService, QuanLyPhieuTraService>();
//builder.Services.AddTransient<QuanLyPhieuMuonService, QuanLyPhieuMuonService>();

builder.Services.AddTransient<KhoSachService, KhoSachService>();
builder.Services.AddTransient<NhapSachService, NhapSachService>();
builder.Services.AddTransient<ThanhLySachService, ThanhLySachService>();
builder.Services.AddTransient<TheDocGiaService, TheDocGiaService>();
builder.Services.AddTransient<ThongTinDocGiaService, ThongTinDocGiaService>();
builder.Services.AddTransient<AccountService, AccountService>();
builder.Services.AddTransient<DangKyMuonSachService, DangKyMuonSachService>();
builder.Services.AddScoped<JwtService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
