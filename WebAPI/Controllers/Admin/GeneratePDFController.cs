using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class GeneratePDFController : ControllerBase
    {
        private readonly GeneratePDFService _generatePDFService; 

        public GeneratePDFController(GeneratePDFService generatePDFService)
        {
            _generatePDFService = generatePDFService;
        }

        [HttpPost]
        public IActionResult GenerateTheDocGiaPDF([FromBody] DTO_DocGia_TheDocGia tdg)
        {
            var document = _generatePDFService.GenerateTheDocGiaPDF(tdg);
            return File(document, "application/pdf", "Hóa đơn tạo thẻ.pdf");
        }

    }
}
