using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Data;

namespace WebAPI.Helper
{
    public static class ExcelHelper
    {
        public static (IList<T>, string) ReadExcelTo<T>(Stream streamfile, DataTable dt) where T : class, new()
        {
            var result = new List<T>();
            try
            {
                
                    //Mở file Excel
                    XLWorkbook package = new XLWorkbook(streamfile);
                    // BRD only read first sheet
                    var sheet = package.Worksheets.FirstOrDefault();

                    var column = sheet?.Columns().Count() ?? 0;
                    if (column != dt.Columns.Count)
                    {
                        return (new List<T>(), "Template không hợp lệ. Vui lòng xử dụng đúng Template!");
                    }

                    var dataSheet = sheet?.ConvertTo<T>(dt);
                    if (dataSheet != null && dataSheet.Count > 0)
                    {
                        result.AddRange(dataSheet);
                    }
                
                // Sau khi đọc xong, đặt lại con trỏ
                streamfile.Position = 0;
                return (result, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.ToString());
            }
        }

        public static IList<T> ConvertTo<T>(this IXLWorksheet sheet, DataTable dt)
        {
            bool FirstRow = true;
            //Range for reading the cells based on the last cell used.
            string readRange = "1:1";
            foreach (IXLRow row in sheet.RowsUsed())
            {
                //If Reading the First Row (used) then add them as column name
                if (FirstRow)
                {
                    //Checking the Last cellused for column generation in datatable
                    //row.LastCellUsed().Address.ColumnNumber
                    readRange = string.Format("{0}:{1}", 1, 10);
                    FirstRow = false;
                }
                else
                {
                    if (row.RowNumber() <= 5)
                    {
                        continue;
                    }

                    //Adding a Row in datatable
                    dt.Rows.Add();
                    int cellIndex = 0;
                    //Updating the values of datatable
                    foreach (IXLCell cell in row.Cells(readRange))
                    {
                        dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.GetFormattedString();
                        cellIndex++;
                    }
                }
            }
            //If no data in Excel file
            if (FirstRow)
            {
                return null;
            }
            var jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
