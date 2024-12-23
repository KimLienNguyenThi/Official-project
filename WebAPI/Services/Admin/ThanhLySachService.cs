using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class ThanhLySachService
    {
        private readonly QuanLyThuVienContext _context;

        public ThanhLySachService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<DonViTl>> GetAllDonViTLPaging(GetListPhieuMuonPaging req)
        {
            var query =
                (from DonViTl in _context.DonViTls
                 where string.IsNullOrEmpty(req.Keyword) || DonViTl.Tendv.Contains(req.Keyword)
                 select new DonViTl
                 {
                     Madv = DonViTl.Madv,
                     Tendv = DonViTl.Tendv,
                     Diachidv = DonViTl.Diachidv,
                     Sdtdv = DonViTl.Sdtdv,

                 });

            var totalRow = await query.CountAsync();

            var listDonvis = await query.OrderByDescending(x => x.Madv).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToListAsync();

            return new PagingResult<DonViTl>
            {
                Results = listDonvis,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }

        public async Task<PagingResult<KhoSachThanhLyDTO>> GetAllKhoTLPaging(GetListPhieuMuonPaging req)
        {
            var query =
                from KhoSachThanhLy in _context.KhoSachThanhLies
                join Sach in _context.Saches on KhoSachThanhLy.Masachkho equals Sach.Masach
                join CHITIETPN in _context.Chitietpns on KhoSachThanhLy.Masachkho equals CHITIETPN.Masach
                where string.IsNullOrEmpty(req.Keyword) || Sach.Tensach.Contains(req.Keyword)
                select new
                {
                    KhoSachThanhLy.Masachkho,
                    Sach.Tensach,
                    SoLuongKhoTL = KhoSachThanhLy.Soluongkhotl.Value,
                    GiaSachTL = (decimal)(CHITIETPN.Giasach.Value * 30 / 100),
                    ChiTietCuonSachs = _context.ChitietKhoThanhLies
                                               .Where(ct => ct.Masachkho == KhoSachThanhLy.Masachkho && ct.Tinhtrang == 0)
                                               .Select(ct => new DTO_ChitietkhoSachTl
                                               {
                                                   Macuonsach = ct.Macuonsach,
                                                   Tinhtrang = ct.Tinhtrang
                                               })
                                               .Distinct()
                                               .ToList()
                };

            var groupedQuery = query
                .GroupBy(group => new { group.Masachkho, group.Tensach, group.SoLuongKhoTL })
                .Select(x => new KhoSachThanhLyDTO
                {
                    MaSachKho = x.Key.Masachkho,
                    TenSach = x.Key.Tensach,
                    SoLuongKhoTL = x.Key.SoLuongKhoTL,
                    GiaSachTL = x.OrderByDescending(item => item.GiaSachTL).First().GiaSachTL,
                    listchitietkhoSachTL = x.SelectMany(item => item.ChiTietCuonSachs).Distinct().ToList()
                })
                .Where(x => x.SoLuongKhoTL > 0)
                .AsQueryable();

            var totalRow = await groupedQuery.CountAsync();

            var listsachs = await groupedQuery.OrderBy(x => x.MaSachKho)
                                              .Skip((req.Page - 1) * req.PageSize)
                                              .Take(req.PageSize)
                                              .ToListAsync();

            return new PagingResult<KhoSachThanhLyDTO>
            {
                Results = listsachs,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }


        public async Task<PagingResult<DTO_Sach_Nhap_Kho>> GetAllSachNhapPaging(GetListPhieuMuonPaging req)
        {

            var query =
              (from Sach in _context.Saches
               where Sach.Soluonghientai > 0 && (
                  string.IsNullOrEmpty(req.Keyword) || Sach.Tensach.Contains(req.Keyword))
               select new DTO_Sach_Nhap_Kho
               {
                   MaSach = Sach.Masach,
                   TenSach = Sach.Tensach,
                   SoLuongHienTai = Sach.Soluonghientai.Value,
               }
                );
            var totalRow = await query.CountAsync();

            var listsachs = await query.OrderBy(x => x.MaSach)
                                       .Skip((req.Page - 1) * req.PageSize)
                                       .Take(req.PageSize)
                                       .ToListAsync();

            return new PagingResult<DTO_Sach_Nhap_Kho>
            {
                Results = listsachs,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };

        }

        ///
        public InsertRes Insertdonvi(DonViTl obj)
        {
            var res = new InsertRes()
            {
                success = false,
                errorCode = -1,
                message = "Thêm đơn vị không thành công."
            };

            if (obj.Tendv == "" || obj.Sdtdv == "" || obj.Diachidv == "")
            {
                return res;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingDonVi = _context.DonViTls.FirstOrDefault(dv => dv.Sdtdv == obj.Sdtdv);

                    if (existingDonVi != null)
                    {
                        //throw new Exception("existingDonVi");
                        res.errorCode = -2;
                        res.message = "Số điện thoại đã tồn tại.";
                        return res;
                    }
                    else
                    {
                        var newDonVi = new DonViTl();
                        {

                            newDonVi.Madv = obj.Madv;
                            newDonVi.Tendv = obj.Tendv;
                            newDonVi.Diachidv = obj.Diachidv;
                            newDonVi.Sdtdv = obj.Sdtdv;
                        };

                        _context.DonViTls.Add(newDonVi);

                        _context.SaveChanges();
                        transaction.Commit();


                        res.success = true;
                        res.message = "Thêm Thành công";
                        res.errorCode = 0;
                        return res;
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi và ghi log
                    return res;
                }
            }

        }

        public bool Insertsach(SachNhapKhoDTO x)
        {
            if (x.MaSachKho <= 0 || x.SoLuongKhoTL <= 0)
            {
                return false;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var sachthanhly = _context.KhoSachThanhLies.FirstOrDefault(p => p.Masachkho == x.MaSachKho);

                    if (sachthanhly != null)
                    {
                        // Cập nhật KhoSachThanhLy
                        sachthanhly.Soluongkhotl += x.SoLuongKhoTL;
                        _context.KhoSachThanhLies.Update(sachthanhly);

                        // Cập nhật Sach
                        var sach = _context.Saches.FirstOrDefault(s => s.Masach == x.MaSachKho);
                        if (sach != null)
                        {
                            sach.Soluonghientai -= x.SoLuongKhoTL;
                            _context.Saches.Update(sach);
                        }

                        transaction.Commit();
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var newsachtl = new KhoSachThanhLy()
                        {
                            Masachkho = x.MaSachKho,
                            Soluongkhotl = x.SoLuongKhoTL,
                        };

                        _context.KhoSachThanhLies.Add(newsachtl);

                        // Cập nhật Sach
                        var sach = _context.Saches.FirstOrDefault(s => s.Masach == x.MaSachKho);
                        if (sach != null)
                        {
                            sach.Soluonghientai -= x.SoLuongKhoTL;
                            _context.Saches.Update(sach);
                        }

                        transaction.Commit();
                        _context.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi và ghi log
                    return false;
                }
            }

        }


        public bool InsertPhieuThanhLy(DTO_Tao_Phieu_TL data)
        {

            if (data.listSachTL.Any(sach => sach.SoLuong > 0) == false)
            {
                return false;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Kiểm tra xem MaNV có tồn tại trong bảng NhanVien không
                    var nhanVien = _context.NhanViens.Find(data.MaNhanVien);
                    if (nhanVien == null)
                    {
                        throw new Exception($"NhanVien with MaNV = {data.MaNhanVien} does not exist.");
                    }
                    // Tạo đối tượng PhieuTra từ DTO_Tao_Phieu_Tra
                    var newPhieuThanhLy = new PhieuThanhLy
                    {
                        Ngaytl = data.NgayTL,
                        Madv = data.MaDonVi,
                        Manv = data.MaNhanVien,
                    };

                    // Thêm PhieuTra vào Context
                    _context.PhieuThanhLies.Add(newPhieuThanhLy);
                    _context.SaveChanges(); // Lưu để có thể lấy 
                    // Duyệt qua danh sách sách trả và tạo đối tượng ChiTietPT cho mỗi cuốn sách
                    foreach (var sachThanhLy in data.listSachTL)
                    {
                        if (sachThanhLy.SoLuong <= 0)
                        {
                            continue;
                        }

                        var chiTietPTL = new ChiTietPtl
                        {
                            Maptl = newPhieuThanhLy.Maptl,
                            Giatl = sachThanhLy.GiaSach,
                            Masachkho = sachThanhLy.MaSach,
                            Soluongtl = sachThanhLy.SoLuong
                        };

                        // Thêm ChiTietPT vào Context
                        _context.ChiTietPtls.Add(chiTietPTL);
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu khi mọi thứ đã thành công
                    _context.SaveChanges();

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi và ghi log
                    return false;
                }
            }

        }
    }
}
