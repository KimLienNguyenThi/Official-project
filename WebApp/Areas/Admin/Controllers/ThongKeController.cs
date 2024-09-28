using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/thongke")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class ThongKeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public ThongKeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [Route("")]
        public IActionResult Index()
        {
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [Route("Get_TongSachNhap_APP")]
        public IActionResult Get_TongSachNhap_APP()
        {

            try
            {
                ThongKePhieu phieu = new ThongKePhieu();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ThongKe/Get_TongSachNhap_API").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    // Log the response data to understand its structure
                    Console.WriteLine("Response Data: " + data);

                    try
                    {
                        // Deserialize the response directly to List<ThongKeSach>
                        phieu = JsonConvert.DeserializeObject<ThongKePhieu>(data);

                        return Ok(new { success = true, phieuList = phieu });
                    }
                    catch (JsonException ex)
                    {
                        // Log the deserialization error for debugging
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return StatusCode(500, new { success = false, message = "Error processing API response." });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }


            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }


        }


        [HttpPost]
        [Route("Get_TongSach_HienTai_APP")]
        public IActionResult Get_TongSach_HienTai_APP()
        {

            try
            {
                ThongKePhieu phieu = new ThongKePhieu();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ThongKe/Get_TongSach_HienTai_API").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    // Log the response data to understand its structure
                    Console.WriteLine("Response Data: " + data);

                    try
                    {
                        // Deserialize the response directly to List<ThongKeSach>
                        phieu = JsonConvert.DeserializeObject<ThongKePhieu>(data);

                        return Ok(new { success = true, phieuList = phieu });
                    }
                    catch (JsonException ex)
                    {
                        // Log the deserialization error for debugging
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return StatusCode(500, new { success = false, message = "Error processing API response." });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }


            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }


        }

        [HttpPost]
        [Route("Get_TongSachMuon_APP")]
        public IActionResult Get_TongSachMuon_APP()
        {

            try
            {
                ThongKePhieu phieu = new ThongKePhieu();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ThongKe/Get_TongSachMuon_API").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    // Log the response data to understand its structure
                    Console.WriteLine("Response Data: " + data);

                    try
                    {
                        // Deserialize the response directly to List<ThongKeSach>
                        phieu = JsonConvert.DeserializeObject<ThongKePhieu>(data);

                        return Ok(new { success = true, phieuList = phieu });
                    }
                    catch (JsonException ex)
                    {
                        // Log the deserialization error for debugging
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return StatusCode(500, new { success = false, message = "Error processing API response." });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }


            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }


        }

        [HttpPost]
        [Route("Get_SachNhap_APP/{tungay}/{denngay}")]
        public IActionResult Get_SachNhap_APP(string tungay, string denngay)
        {
            try
            {
                List<ThongKeSach> bookList = new List<ThongKeSach>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_SachNhap_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            bookList = JsonConvert.DeserializeObject<List<ThongKeSach>>(data);

                            return Ok(new { success = true, sachList = bookList });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [Route("Get_SachMuon_APP/{tungay}/{denngay}")]
        public IActionResult Get_SachMuon_APP(string tungay, string denngay)
        {

            try
            {
                List<ThongKeSach> bookList = new List<ThongKeSach>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_SachMuon_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            bookList = JsonConvert.DeserializeObject<List<ThongKeSach>>(data);

                            return Ok(new { success = true, sachList = bookList });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        [Route("Get_PhieuMuon_APP/{tungay}/{denngay}")]
        public IActionResult Get_PhieuMuon_APP(string tungay, string denngay)
        {

            try
            {
                ThongKePhieu phieu = new ThongKePhieu();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_PhieuMuon_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            phieu = JsonConvert.DeserializeObject<ThongKePhieu>(data);

                            return Ok(new { success = true, phieuList = phieu });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }


        }


        [HttpPost]
        [Route("Get_ListPM_APP/{tungay}/{denngay}")]
        public IActionResult Get_ListPM_APP(string tungay, string denngay)
        {
            try
            {
                List<ThongKePM> phieu = new List<ThongKePM>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_ListPM_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            phieu = JsonConvert.DeserializeObject<List<ThongKePM>>(data);

                            return Ok(new { success = true, phieuList = phieu });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [Route("Get_PhieuTra_APP/{tungay}/{denngay}")]
        public IActionResult Get_PhieuTra_APP(string tungay, string denngay)
        {

            try
            {
                ThongKePhieu phieu = new ThongKePhieu();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_PhieuTra_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            phieu = JsonConvert.DeserializeObject<ThongKePhieu>(data);

                            return Ok(new { success = true, phieuList = phieu });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        [Route("Get_ListPT_APP/{tungay}/{denngay}")]
        public IActionResult Get_ListPT_APP(string tungay, string denngay)
        {
            try
            {
                List<ThongKePT> phieu = new List<ThongKePT>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_ListPT_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            phieu = JsonConvert.DeserializeObject<List<ThongKePT>>(data);

                            return Ok(new { success = true, phieuList = phieu });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [Route("Get_DocGiaMuon_APP/{tungay}/{denngay}")]
        public IActionResult Get_DocGiaMuon_APP(string tungay, string denngay)
        {

            try
            {
                List<ThongKeDocGia_Muon> dg = new List<ThongKeDocGia_Muon>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_DocGiaMuon_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            dg = JsonConvert.DeserializeObject<List<ThongKeDocGia_Muon>>(data);

                            return Ok(new { success = true, dgList = dg });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        [Route("Get_DocGiaDki_APP/{tungay}/{denngay}")]
        public IActionResult Get_DocGiaDki_APP(string tungay, string denngay)
        {

            try
            {
                List<ThongKeDocGia_Dki> dg = new List<ThongKeDocGia_Dki>();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_DocGiaDki_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            dg = JsonConvert.DeserializeObject<List<ThongKeDocGia_Dki>>(data);

                            return Ok(new { success = true, dgList = dg });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }
        

        [HttpPost]
        [Route("Get_Money_APP/{tungay}/{denngay}")]
        public async Task<ActionResult> Get_Money_APP(string tungay, string denngay)
        {
            try
            {
                ThongKeDoanhThu doanhthu = new ThongKeDoanhThu();
                if (DateTime.TryParse(tungay, out DateTime tungayDate) && DateTime.TryParse(denngay, out DateTime denngayDate))
                {
                    DateOnly tungayDateOnly = new DateOnly(tungayDate.Year, tungayDate.Month, tungayDate.Day);
                    DateOnly denngayDateOnly = new DateOnly(denngayDate.Year, denngayDate.Month, denngayDate.Day);

                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/ThongKe/Get_Money_API/{tungay}/{denngay}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        // Log the response data to understand its structure
                        Console.WriteLine("Response Data: " + data);

                        try
                        {
                            // Deserialize the response directly to List<ThongKeSach>
                            doanhthu = JsonConvert.DeserializeObject<ThongKeDoanhThu>(data);

                            return Ok(new { success = true, doanhthuList = doanhthu });
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error for debugging
                            Console.WriteLine("Deserialization error: " + ex.Message);
                            return StatusCode(500, new { success = false, message = "Error processing API response." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                    }
                }
                else
                {
                    return BadRequest("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

    }
}
