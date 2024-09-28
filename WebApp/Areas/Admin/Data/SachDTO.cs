using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApp.Areas.Admin.Data
{
    public class SachDTO
    {
        public int MaSach { get; set; }

        public string? TenSach { get; set; }

        public string? TheLoai { get; set; }

        public string? TacGia { get; set; }

        public string? NgonNgu { get; set; }

        public string? Nxb { get; set; }

        public int? NamXb { get; set; }

        public int? SoLuongHientai { get; set; }

        public string? UrlImage { get; set; }

        public string? Mota { get; set; }
       
    }


   

}