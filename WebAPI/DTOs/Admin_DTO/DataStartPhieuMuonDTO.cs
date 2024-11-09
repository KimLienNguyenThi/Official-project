using WebAPI.DTOs;
using WebAPI.DTOs.Admin;
using WebAPI.Models;
using WebAPI.Areas.Admin.Data;

namespace WebAPI.Areas.Admin.Data
{
    public class DataStartPhieuMuonDTO
    {
        public List<DTO_DocGia_TheDocGia> docGia { get; set; }
        public List<Sach> sach { get; set; }
        public List<DKiMuonSachDTO_PM> dKiMuonSach { get; set; }
    }
}
