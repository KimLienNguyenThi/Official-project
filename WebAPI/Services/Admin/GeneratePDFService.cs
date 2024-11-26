using WebAPI.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Companion;
using System.Drawing;
using WebAPI.Areas.Admin.Data;

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
    }
}
