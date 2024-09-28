using WebApp.DTOs;
using WebApp.DTOs.Admin;
using WebApp.Models;

namespace WebApp.Areas.Admin.Data
{
    public class DataStartPhieuMuonDTO
    {
        public List<DTO_DocGia_TheDocGia> docGia { get; set; }
        public List<SachDTO> sach { get; set; }
        public List<DKiMuonSachDTO_PM> dKiMuonSach { get; set; }
    }
}
