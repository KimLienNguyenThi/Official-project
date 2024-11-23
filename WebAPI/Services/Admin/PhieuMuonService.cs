using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class PhieuMuonService
    {
        private readonly QuanLyThuVienContext _context;

        public PhieuMuonService(QuanLyThuVienContext context)
        {
            _context = context;
        }

       
    }
}
