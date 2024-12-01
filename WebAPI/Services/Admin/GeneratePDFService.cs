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
                throw new Exception($"Không tìm thấy dữ liệu cho phiếu trả: {tpt.MaPhieuMuon}");
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

                        row.RelativeItem().Text("Hóa đơn trả sách").AlignRight().FontFamily("Arial").FontSize(20).Bold();
                    });

                    // Content
                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        // Thông tin phiếu trả
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item().Text("Thông tin phiếu trả").Bold();
                                column2.Item().Text($"Mã phiếu trả: {mapt}").FontSize(12);
                                column2.Item().Text($"Tên độc giả: {tpt.TenDG}").FontSize(12);
                                column2.Item().Text($"Ngày trả: {tpt.NgayTra?.ToString("dd/MM/yyyy") ?? "Chưa xác định"}").FontSize(12);
                            });
                        });

                        // Bảng chi tiết sách trả
                        column.Item().PaddingTop(10).Table(table =>
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
                                header.Cell().Text("Mã sách").FontSize(12).Bold();
                                header.Cell().Text("Tên sách").FontSize(12).Bold();
                                header.Cell().Text("S/Lượng mượn").FontSize(12).Bold().AlignCenter();
                                header.Cell().Text("S/Lượng trả").FontSize(12).Bold().AlignCenter();
                                header.Cell().Text("S/Lượng lỗi").FontSize(12).Bold().AlignCenter();
                                header.Cell().Text("S/Lượng mất").FontSize(12).Bold().AlignCenter();
                                header.Cell().Text("Phụ thu").FontSize(12).Bold().AlignCenter();

                                header.Cell().ColumnSpan(7).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            // Dữ liệu bảng
                            foreach (var sachTra in tpt.ListSachTra)
                            {
                                table.Cell().Text(sachTra.MaSach.ToString());
                                table.Cell().Text(sachTra.TenSach);
                                table.Cell().Text(sachTra.SoLuongMuon.ToString()).AlignCenter();
                                table.Cell().Text(sachTra.SoLuongTra.ToString()).AlignCenter();
                                table.Cell().Text(sachTra.SoLuongLoi.ToString()).AlignCenter();
                                table.Cell().Text(sachTra.SoLuongMat.ToString()).AlignCenter();
                                table.Cell().Text(sachTra.PhuThu.ToString("N2")).AlignRight();

                                // Chi tiết cuốn sách
                                foreach (var ctSachTra in sachTra.ListCTSachTra)
                                {
                                    table.Cell().ColumnSpan(7).PaddingLeft(10).Text(
                                        $"Mã cuốn sách: {ctSachTra.MaCuonSach}, Tình trạng: {(ctSachTra.Tinhtrang == 1 ? "Bình Thường" : (ctSachTra.Tinhtrang == 2 ? "Lỗi" : "Mất"))}")
                                        .FontSize(10).Italic();
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


    }
}
