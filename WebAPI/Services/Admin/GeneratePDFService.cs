using WebAPI.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Companion;
using System.Drawing;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;

namespace WebAPI.Services.Admin
{
    public class GeneratePDFService
    {
        public GeneratePDFService()
        {
            
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        }
        public byte[] GenerateTheDocGiaPDF(DTO_DocGia_TheDocGia tdg)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);


                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("Thư viện ABC").FontFamily("Arial").FontSize(20).Bold();
                            column.Item().Text("450 Lê Văn Việt, Thành Phố Thủ Đức").FontFamily("Arial");
                        });

                        row.RelativeItem().ShowOnce().Text("Hóa đơn tạo thẻ").AlignRight().FontFamily("Arial").FontSize(20).Bold();
                    });


                    page.Content().PaddingTop(20).Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item().Text("Hóa đơn cho khách hàng").Bold();
                                column2.Item().Text($"Tên khách hàng: {tdg.HoTenDG}").FontFamily("Arial").FontSize(15).Bold();
                            });

                            //row.RelativeItem().Column(column2 =>
                            //{
                            //    column2.Item().Text("Mã thẻ: ").AlignRight().Bold();
                            //    column2.Item().PaddingTop(2).Text("Ngày tạo: ").AlignRight();
                            //});
                        });

                        column.Item().PaddingTop(30).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);
                                columns.RelativeColumn();
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Mã thẻ").Bold();
                                header.Cell().Text("Họ và tên chủ thẻ").Bold();
                                header.Cell().Text("Số tiền").Bold().AlignRight();
                                header.Cell().Text("Ngày đăng ký").Bold().AlignRight();
                                header.Cell().Text("Ngày hết hạn").Bold().AlignRight();

                                header.Cell().ColumnSpan(5).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            table.Cell().Padding(4).Text(tdg.MaThe);
                            table.Cell().Padding(4).Text(tdg.HoTenDG);
                            table.Cell().Padding(4).AlignRight().Text(tdg.TienThe);
                            table.Cell().Padding(4).AlignRight().Text(tdg.NgayDangKy);
                            table.Cell().Padding(4).AlignRight().Text(tdg.NgayHetHan);

                            table.Cell().ColumnSpan(5).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);

                        });

                        column.Item().Column(column =>
                        {
                            column.Item().PaddingTop(30).Text("Vui lòng kiểm tra lại thông tin trước khi rời khỏi quầy").FontFamily("Arial").FontSize(15).Italic();
                        });
                    });


                    page.Footer().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Thư viện ABC trân trọng cảm ơn quý khách");
                    });
                });
            });

            return document.GeneratePdf();
        }
        public byte[] GeneratePhieuTraPDF(DTO_Tao_Phieu_Tra tpt, int mapt)
        {
            if (tpt == null || tpt.ListSachTra == null || !tpt.ListSachTra.Any())
            {
                throw new Exception($"Không tìm thấy dữ liệu cho phiếu trả: {mapt}");
            }

            // Tính tổng tiền
            decimal tongTien = tpt.ListSachTra.Sum(sach => sach.PhuThu);

            // Tạo file PDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    // Header
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("Thư viện ABC").FontFamily("Arial").FontSize(20).Bold();
                            column.Item().Text("450 Lê Văn Việt, Thành Phố Thủ Đức").FontFamily("Arial");
                        });

                        row.RelativeItem().Text("Hóa đơn trả sách").AlignRight().FontFamily("Arial").FontSize(20).Bold();
                    });

                    // Content
                    page.Content().PaddingTop(20).Column(column =>
                    {
                        // Thông tin phiếu trả
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item().Text("Thông tin phiếu trả").Bold();
                                column2.Item().Text($"Mã phiếu trả: {mapt}").FontSize(15);
                                column2.Item().Text($"Tên độc giả: {tpt.TenDG}").FontSize(15);
                                column2.Item().Text($"Ngày trả: {tpt.NgayTra?.ToString("dd/MM/yyyy") ?? "Chưa xác định"}").FontSize(15);
                            });
                        });

                        // Bảng chi tiết sách trả
                        column.Item().PaddingTop(30).Table(table =>
                        {

                            // Định nghĩa cột
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);  // Mã sách
                                columns.RelativeColumn(3);  // Tên sách
                                columns.RelativeColumn(1);  // Số lượng mượn
                                columns.RelativeColumn(1);  // Số lượng trả
                                columns.RelativeColumn(1);  // Số lượng lỗi
                                columns.RelativeColumn(1);  // Số lượng mất
                                columns.RelativeColumn(1);  // Phụ thu
                            });

                            // Header bảng
                            table.Header(header =>
                            {
                                header.Cell().Text("Mã sách").Bold();
                                header.Cell().Text("Tên sách").Bold();
                                header.Cell().Text("S/Lượng mượn").Bold().AlignCenter();
                                header.Cell().Text("S/Lượng trả").Bold().AlignCenter();
                                header.Cell().Text("S/Lượng lỗi").Bold().AlignCenter();
                                header.Cell().Text("S/Lượng mất").Bold().AlignCenter();
                                header.Cell().Text("Phụ thu").Bold().AlignCenter();

                                header.Cell().ColumnSpan(7).PaddingVertical(6).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            // Dữ liệu bảng
                            foreach (var sachTra in tpt.ListSachTra)
                            {

                                table.Cell().Padding(4).Text(sachTra.MaSach.ToString());
                                table.Cell().Padding(4).Text(sachTra.TenSach);
                                table.Cell().Padding(4).Text(sachTra.SoLuongMuon.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachTra.SoLuongTra.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachTra.SoLuongLoi.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachTra.SoLuongMat.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachTra.PhuThu.ToString("#,##0")).AlignRight(); // Định dạng bỏ .00

                                // Chi tiết cuốn sách
                                foreach (var ctSachTra in sachTra.ListCTSachTra)
                                {
                                    table.Cell().ColumnSpan(7).PaddingLeft(20).Text(
                                        $"      Mã cuốn sách: {ctSachTra.MaCuonSach}           Tình trạng: {(ctSachTra.Tinhtrang == 1 ? "Bình Thường" : (ctSachTra.Tinhtrang == 2 ? "Lỗi" : "Mất"))}")
                                        .FontSize(13).Bold();
                                }
                            }

                            // Thêm dòng tổng tiền
                            table.Cell().ColumnSpan(6).Text("Tổng tiền").Bold().AlignRight();
                            table.Cell().Text(tongTien.ToString("#,##0")).Bold().AlignRight(); // Định dạng bỏ .00

                            table.Cell().ColumnSpan(7).PaddingVertical(6).BorderBottom(1).BorderColor(Colors.Black);
                        });

                        column.Item().PaddingTop(30).Text("Vui lòng kiểm tra lại thông tin trước khi rời khỏi quầy")
                              .FontFamily("Arial").FontSize(15).Italic();
                    });

                    // Footer
                    page.Footer().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Thư viện ABC trân trọng cảm ơn quý khách");
                    });
                });
            });

            return document.GeneratePdf();
        }

        public byte[] GeneratePhieuMuonPDF(DTO_Tao_Phieu_Muon tpm, int mapm)
        {
            if (tpm == null || tpm.listSachMuon == null || tpm.listCTSachMuon == null)
            {
                throw new Exception($"Không tìm thấy dữ liệu cho phiếu mượn: {mapm}");
            }

            // Tạo file PDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    // Header
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("Thư viện ABC").FontFamily("Arial").FontSize(20).Bold();
                            column.Item().Text("450 Lê Văn Việt, Thành Phố Thủ Đức").FontFamily("Arial");
                        });

                        row.RelativeItem().Text("Phiếu mượn sách").AlignRight().FontFamily("Arial").FontSize(20).Bold();
                    });

                    // Content
                    page.Content().PaddingTop(20).Column(column =>
                    {
                        // Thông tin phiếu mượn
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(infoCol =>
                            {
                                infoCol.Item().Text("Thông tin phiếu mượn").Bold();
                                infoCol.Item().Text($"Mã phiếu mượn: {mapm}").FontSize(15);
                                infoCol.Item().Text($"Mã thẻ độc giả: {tpm.MaTheDocGia}").FontSize(15);
                                infoCol.Item().Text($"Tên độc giả: {tpm.TenDocGia}").FontSize(15);
                                infoCol.Item().Text($"Mã nhân viên: {tpm.MaNhanVien}").FontSize(15);
                                infoCol.Item().Text($"Ngày mượn: {tpm.NgayMuon:dd/MM/yyyy}").FontSize(15);
                                infoCol.Item().Text($"Hạn trả: {tpm.NgayTra:dd/MM/yyyy}").FontSize(15);
                            });
                        });

                        // Hiển thị danh sách sách mượn
                        column.Item().PaddingTop(30).Table(table =>
                        {
                            // Định nghĩa cột
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);  // Mã sách
                                columns.RelativeColumn(4);  // Tên sách
                                columns.RelativeColumn(3);  // Mã cuốn sách
                                columns.RelativeColumn(1);  // Số lượng
                            });

                            // Header bảng
                            table.Header(header =>
                            {
                                header.Cell().Text("Mã sách").Bold();
                                header.Cell().Text("Tên sách").Bold();
                                header.Cell().Text("Mã cuốn sách").Bold();
                                header.Cell().Text("Số lượng").Bold().AlignCenter();

                                header.Cell().ColumnSpan(4).PaddingVertical(6).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            // Dữ liệu bảng
                            var addedBooks = new HashSet<string>(); // Bộ nhớ để kiểm tra dòng đã được thêm

                            foreach (var sach in tpm.listSachMuon)
                            {
                                var maCuonSachList = tpm.listCTSachMuon
                                    .Where(ct => ct.MaCuonSach.StartsWith(sach.MaSach.ToString()))
                                    .Select(ct => ct.MaCuonSach)
                                    .Distinct()
                                    .ToList();

                                foreach (var maCuonSach in maCuonSachList)
                                {
                                    // Kiểm tra nếu mã cuốn sách đã hiển thị thì bỏ qua
                                    if (addedBooks.Contains(maCuonSach)) continue;

                                    // Thêm mã cuốn sách vào bộ nhớ
                                    addedBooks.Add(maCuonSach);

                                    table.Cell().Padding(4).Text(sach.MaSach.ToString());      // Mã sách
                                    table.Cell().Padding(4).Text(sach.TenSach);                // Tên sách
                                    table.Cell().Padding(4).Text(maCuonSach);                  // Mã cuốn sách
                                    table.Cell().Padding(4).Text("1").AlignCenter();           // Số lượng mặc định là 1
                                }
                            }
                        });

                        column.Item().PaddingTop(30).Text("Vui lòng kiểm tra lại thông tin trước khi rời khỏi quầy")
                              .FontFamily("Arial").FontSize(15).Italic();
                    });

                    // Footer
                    page.Footer().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Thư viện ABC trân trọng cảm ơn quý khách");
                    });
                });
            });

            return document.GeneratePdf();
        }




        public byte[] GeneratePhieuNhapPDF(DTO_Tao_Phieu_Nhap tpn, int mapn)
        {
            if (tpn == null || tpn.listSachNhap == null || !tpn.listSachNhap.Any())
            {
                throw new Exception($"Không tìm thấy dữ liệu cho phiếu nhập : {mapn}");
            }

            // Tính tổng tiền
            decimal tongTien = tpn.listSachNhap.Sum(sach => sach.GiaSach* sach.SoLuong);

            // Tạo file PDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    // Header
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("Thư viện ABC").FontFamily("Arial").FontSize(20).Bold();
                            column.Item().Text("450 Lê Văn Việt, Thành Phố Thủ Đức").FontFamily("Arial");
                        });

                        row.RelativeItem().Text("Hóa đơn nhập sách").AlignRight().FontFamily("Arial").FontSize(20).Bold();
                    });

                    // Content
                    page.Content().PaddingTop(20).Column(column =>
                    {
                        // Thông tin phiếu trả
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item().Text("Thông tin Phiếu nhập").Bold();
                                column2.Item().Text($"Mã phiếu nhập: {mapn}").FontSize(15);
                                column2.Item().Text($"Tên Nhà cung cấp: {tpn.TenNhaCungCap}").FontSize(15);
                                column2.Item().Text($"Ngày Nhập: {tpn.NgayNhap?.ToString("dd/MM/yyyy") ?? "Chưa xác định"}").FontSize(15);
                            });
                        });

                        // Bảng chi tiết sách trả
                        column.Item().PaddingTop(30).Table(table =>
                        {
                            // Định nghĩa cột
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);  // Mã sách
                                columns.RelativeColumn(3);  // Tên sách
                                columns.RelativeColumn(1);  // Thể loại
                                columns.RelativeColumn(1);  // NgonNgu
                                columns.RelativeColumn(1);  // TacGia
                                columns.RelativeColumn(1);  // SoLuong
                                columns.RelativeColumn(1);  // GiaSach
                            });

                            // Header bảng
                            table.Header(header =>
                            {
                                header.Cell().Text("Mã sách").Bold();
                                header.Cell().Text("Tên sách").Bold();
                                header.Cell().Text("Thể loại").Bold().AlignCenter();
                                header.Cell().Text("NgonNgu").Bold().AlignCenter();
                                header.Cell().Text("Tác Giả").Bold().AlignCenter();
                                header.Cell().Text("Số Lượng").Bold().AlignCenter();
                                header.Cell().Text("Giá Sách").Bold().AlignCenter();

                                header.Cell().ColumnSpan(7).PaddingVertical(6).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            // Dữ liệu bảng
                            foreach (var sachNhap in tpn.listSachNhap)
                            {

                                table.Cell().Padding(4).Text(sachNhap.MaSach.ToString());
                                table.Cell().Padding(4).Text(sachNhap.TenSach);
                                table.Cell().Padding(4).Text(sachNhap.TheLoai.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachNhap.NgonNgu.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachNhap.TacGia.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachNhap.SoLuong.ToString()).AlignCenter();
                                table.Cell().Padding(4).Text(sachNhap.GiaSach.ToString("#,##0")).AlignRight(); // Định dạng bỏ .00
                            }

                            // Thêm dòng tổng tiền
                            table.Cell().ColumnSpan(6).Text("Tổng tiền").Bold().AlignRight();
                            table.Cell().Text(tongTien.ToString("#,##0")).Bold().AlignRight(); // Định dạng bỏ .00

                            table.Cell().ColumnSpan(7).PaddingVertical(6).BorderBottom(1).BorderColor(Colors.Black);
                        });

                        column.Item().PaddingTop(30).Text("Vui lòng kiểm tra lại thông tin trước khi rời khỏi quầy")
                              .FontFamily("Arial").FontSize(15).Italic();
                    });

                    // Footer
                    page.Footer().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Thư viện ABC trân trọng cảm ơn quý khách");
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
