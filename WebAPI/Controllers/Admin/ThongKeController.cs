using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Service_Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]

    public class ThongKeController : ControllerBase
    {
        private readonly ThongKeService _thongkeService;

        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;

        public ThongKeController(IMapper mapper, QuanLyThuVienContext context, ThongKeService thongkeService)
        {
            _context = context;
            _mapper = mapper;
            _thongkeService = thongkeService;

        }
        

        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_SachMuon_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                List<ThongKeSach> sachMuon = _thongkeService.GetSachMuon(tungay, denngay);
                return Ok(sachMuon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
     

        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_PhieuMuon_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                ThongKePhieu phieuMuon = _thongkeService.GetPhieuMuon(tungay, denngay);
                return Ok(phieuMuon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_ListPM_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                List<ThongKePM> phieuMuon = _thongkeService.GetListPM(tungay, denngay);
                return Ok(phieuMuon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_PhieuTra_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                ThongKePhieu phieuTra = _thongkeService.GetPhieuTra(tungay, denngay);
                return Ok(phieuTra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_ListPT_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                List<ThongKePT> phieuMuon = _thongkeService.GetListPT(tungay, denngay);
                return Ok(phieuMuon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_DocGiaMuon_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                List<ThongKeDocGia_Muon> DG = _thongkeService.GetDocGiaMuon(tungay, denngay);
                return Ok(DG);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tungay}/{denngay}")]
        public IActionResult Get_DocGiaDki_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                List<ThongKeDocGia_Dki> DG = _thongkeService.GetDocGiaDki(tungay, denngay);
                return Ok(DG);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{tungay}/{denngay}")]
        public async Task<ActionResult<ThongKeDoanhThu>> Get_Money_API(DateOnly tungay, DateOnly denngay)
        {
            try
            {
                ThongKeDoanhThu DG = await _thongkeService.GetMoney(tungay, denngay);
                return Ok(DG);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
//[HttpGet]
//public IActionResult Get_TongSachNhap_API()
//{
//    try
//    {
//        ThongKePhieu sach = _thongkeService.GetTongSachNhap();
//        return Ok(sach);
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}


//[HttpGet]
//public IActionResult Get_TongSach_HienTai_API()
//{
//    try
//    {
//        ThongKePhieu sach = _thongkeService.GetTongSachHienTai();
//        return Ok(sach);
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}


//[HttpGet]
//public IActionResult Get_TongSachMuon_API()
//{
//    try
//    {
//        ThongKePhieu sach = _thongkeService.GetTongSachMuon();
//        return Ok(sach);
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}

//[HttpGet("{tungay}/{denngay}")]
//public IActionResult Get_SachNhap_API(DateOnly tungay, DateOnly denngay)
//{
//    try
//    {
//        List<ThongKeSach> sachNhap = _thongkeService.GetSachNhap(tungay, denngay);
//        return Ok(sachNhap);
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}