using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using WebApp.Models;

namespace WebApp.Areas.Admin.Data
{
    public class DTO_DocGia_TheDocGia
    {
        public int MaThe { get; set; }
        public int MaDocGia { get; set; }
        public int MaNhanVien { get; set; }
        public string HoTenDG { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public DateOnly NgaySinh { get; set; }
        public DateOnly NgayDangKy { get; set; }
        public DateOnly NgayHetHan { get; set; }
        public decimal TienThe { get; set; }
    }


    public class DTO_NhanVien_LoginNV
    {
        public int MaNV { get; set; }
        public string HoTenNV { get; set; }
        public string SDT { get; set; }
        public string GioiTinh { get; set; }
        public DateOnly? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public static explicit operator DTO_NhanVien_LoginNV(NhanVien v)
        {
            return new DTO_NhanVien_LoginNV
            {
                MaNV = v.MaNv,
                HoTenNV = v.HoTenNv,
                SDT = v.Sdt,
                DiaChi = v.DiaChi,
                GioiTinh = v.GioiTinh,
                NgaySinh = v.Ngaysinh,
                ChucVu = v.ChucVu,
            };
        }
    }

    public class DTO_Sach_Muon
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
    }

    public class DTO_Tao_Phieu_Muon
    {
        public int MaNhanVien { get; set; }
        public int MaTheDocGia { get; set; }
        public DateOnly NgayMuon { get; set; }
        public DateOnly NgayTra { get; set; }
        public int MaDK { get; set; }


        public List<DTO_Sach_Muon> listSachMuon { get; set; }

        public DTO_Tao_Phieu_Muon()
        {
            listSachMuon = new List<DTO_Sach_Muon>();
        }
    }


    public class DTO_Sach_Nhap
    {
        public int maSach { get; set; }
        public string tenSach { get; set; }
        public string theLoai { get; set; }
        public string ngonNgu { get; set; }
        public string tacGia { get; set; }
        public string nhaXB { get; set; }
        public int namXB { get; set; }
        public int soLuong { get; set; }
        public decimal giaSach { get; set; }
        public string? moTa { get; set; }
        public IFormFile? fileImage { get; set; }
    }

    public class DTO_Tao_Phieu_Nhap
    {
        public int MaNhanVien { get; set; }
        public int MaNhaCungCap { get; set; }
        public DateOnly NgayNhap { get; set; }
        public List<DTO_Sach_Nhap> listSachNhap { get; set; }

        //public DTO_Tao_Phieu_Nhap()
        //{
        //    listSachNhap = new List<DTO_Sach_Nhap>();
        //}

    }
    public class DTO_DangKyMuonSach
    {
        public string SDT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaDK { get; set; }
        public DateOnly? NgayDK { get; set; }
        public DateOnly? NgayHen { get; set; }
        public int? TinhTrang { get; set; }
    }

    public class DTO_DangKyMuonSach_GroupSDT
    {
        public string SDT { get; set; }
        public int CountRow { get; set; }
        public List<DTO_DangKyMuonSach> List_DTO_DangKyMuonSach { get; set; }
    }
    public class DTO_DangKyMuonSach_PM
    {
        public int MaThe { get; set; }
        public string SDT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaDK { get; set; }
        public DateOnly? NgayDK { get; set; }
        public DateOnly? NgayHen { get; set; }
        public int? TinhTrang { get; set; }
    }


    public class DTO_Tao_Phieu_Nhap_Json
    {
        public int MaNhanVien { get; set; }
        public int MaNhaCungCap { get; set; }
        public DateOnly NgayNhap { get; set; }
        public List<DTO_Sach_Nhap_Json> listSachNhap { get; set; }
    }
    public class DTO_Sach_Nhap_Json
    {
        public int maSach { get; set; }
        public string tenSach { get; set; }
        public string theLoai { get; set; }
        public string ngonNgu { get; set; }
        public string tacGia { get; set; }
        public string nhaXB { get; set; }
        public int namXB { get; set; }
        public int soLuong { get; set; }
        public decimal giaSach { get; set; }
        public string? moTa { get; set; }

        public string? fileImage { get; set; }
    }
}
