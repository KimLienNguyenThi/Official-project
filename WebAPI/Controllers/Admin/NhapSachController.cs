using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class NhapSachController : Controller
    {
        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        private readonly NhapSachService _nhapSachService;


        public NhapSachController(IMapper mapper, QuanLyThuVienContext context, PhieuTraService phieuMuonService, NhapSachService nhapSachService)
        {

            _context = context;
            _mapper = mapper;
            _nhapSachService = nhapSachService;
        }

        //[HttpPost]
        //public async Task<ActionResult<PagingResult<NhaCungCap>>> GetListNCCPaging_API([FromBody] GetListPhieuMuonPaging req)
        //{
        //    var result = await _nhapSachService.GetAllNCCPaging(req);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public async Task<ActionResult<PagingResult<Sach>>> GetListSachPaging_API([FromBody] GetListPhieuMuonPaging req)
        //{
        //    var result = await _nhapSachService.GetAllSachPaging(req);
        //    return Ok(result);
        //}

        //[HttpPost]
        //public IActionResult ThemNCC_API([FromBody] NhaCungCap data)
        //{
        //    var result = _nhapSachService.InsertNCC(data);

        //    return Ok(result);

        //    // return BadRequest(new { success = false, message = "Thêm NCC thất bại." });
        //}


        //[HttpPost]
        //public IActionResult PhieuNhap_API([FromForm] string data)
        //{
        //    if (string.IsNullOrEmpty(data))
        //    {
        //        return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
        //    }

        //    // Deserialize the data to DTO_Tao_Phieu_Nhap
        //    DTO_Tao_Phieu_Nhap dto = JsonConvert.DeserializeObject<DTO_Tao_Phieu_Nhap>(data);

        //    // Create a list to store image URLs
        //    var imageUrls = new List<string>();

        //    // Loop through the DTO list to get image URLs
        //    foreach (var sach in dto.listSachNhap)
        //    {
        //        // Get the image URL from the DTO object
        //        imageUrls.Add(sach.FileImage);
        //    }

        //    // Call the service to insert data into the database
        //    var success = _nhapSachService.InsertPhieuNhap(dto, imageUrls);
        //    if (success)
        //    {
        //        return Ok(new { success = true, message = "Tạo phiếu nhập thành công." });
        //    }
        //    else
        //    {
        //        return BadRequest(new { success = false, message = "Tạo phiếu nhập thất bại." });
        //    }
        //}
    }
}
