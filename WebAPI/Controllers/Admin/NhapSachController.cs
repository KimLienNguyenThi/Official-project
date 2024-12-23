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


        public NhapSachController(IMapper mapper, QuanLyThuVienContext context,  NhapSachService nhapSachService)
        {

            _context = context;
            _mapper = mapper;
            _nhapSachService = nhapSachService;
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<NhaCungCap>>> GetListNCCPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _nhapSachService.GetAllNCCPaging(req);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<PagingResult<Sach>>> GetListSachPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _nhapSachService.GetAllSachPaging(req);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ThemNCC_API([FromBody] NhaCungCap data)
        {
            var result = _nhapSachService.InsertNCC(data);

            return Ok(result);

            // return BadRequest(new { success = false, message = "Thêm NCC thất bại." });
        }

        [HttpPost]
        public IActionResult PhieuNhap_API([FromForm] string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                // Deserialize the data to DTO_Tao_Phieu_Nhap
                DTO_Tao_Phieu_Nhap dto;
                try
                {
                    dto = JsonConvert.DeserializeObject<DTO_Tao_Phieu_Nhap>(data);
                }
                catch (JsonException ex)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không đúng định dạng.", error = ex.Message });
                }

                // Create a list to store image URLs
                var imageUrls = new List<string>();
                foreach (var sach in dto.listSachNhap)
                {
                    if (!string.IsNullOrEmpty(sach.FileImage))
                    {
                        imageUrls.Add(sach.FileImage);
                    }
                }

                // Call the service to insert data into the database and generate PDF
                var pdfData = _nhapSachService.InsertPhieuNhap(dto, imageUrls);
                if (pdfData == null || pdfData.Length == 0)
                {
                    return StatusCode(500, new { success = false, message = "Không thể tạo file PDF." });
                }

                // Generate PDF file name
                string fileName = $"PhieuNhap_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                // Return the PDF file as response
                return File(pdfData, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo phiếu nhập.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetNamXBMax_API()
        {
            var result = _nhapSachService.GetNamXBMax();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<NhaCungCap>> GetListNCC_API(int mancc)
        {
            var result = await _nhapSachService.GetAllNCC(mancc);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { Message = "File không hợp lệ hoặc bị trống." });
            }

            // Kiểm tra định dạng tệp
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest(new { Message = "Chỉ chấp nhận file Excel với định dạng .xlsx hoặc .xls." });
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    // Gọi service để xử lý file
                    var (successfulRecords, errorRecords) = _nhapSachService.ProcessExcelFile(stream);

                    // Lưu các bản ghi hợp lệ và lỗi vào bảng tạm
                    var allRecords = successfulRecords.Concat(errorRecords).ToList();
                    _nhapSachService.SaveToTempTable(allRecords);

                    // Nếu toàn bộ đều lỗi, thông báo rõ ràng
                    if (successfulRecords.Count == 0 && errorRecords.Count > 0)
                    {
                        return Ok(new
                        {
                            Message = "Toàn bộ file chứa lỗi, vui lòng kiểm tra và sửa lại.",
                            SuccessfulRecords = successfulRecords,
                            ErrorRecords = errorRecords
                        });
                    }

                    // Nếu có dòng hợp lệ và dòng lỗi
                    return Ok(new
                    {
                        Message = "File đã được xử lý thành công. Một số dòng chứa lỗi cần được chỉnh sửa.",
                        SuccessfulRecords = successfulRecords,
                        ErrorRecords = errorRecords
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Có lỗi xảy ra trong quá trình xử lý file.",
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult PhieuNhap_Excel_API(DTO_Tao_Phieu_Nhap_Excel obj)
        {
            try
            {
                if (obj == null)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                // Call the service to insert data into the database
                bool result = _nhapSachService.InsertPhieuNhapByExcel(obj);
                if (!result)
                {
                    return StatusCode(500, new { success = false, message = "Không thể tạo phiếu nhập." });
                }

                // Return success response
                return Ok(new { success = true, message = "Phiếu nhập đã được tạo thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo phiếu nhập.", error = ex.Message });
            }
        }


    }
}
