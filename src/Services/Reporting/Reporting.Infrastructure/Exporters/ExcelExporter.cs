


using ClosedXML.Excel;

namespace Reporting.Infrastructure.Exporters;

public class ExcelExporter : IReportExporter
{
    public ReportFormat Format => ReportFormat.Excel;

    public byte[] Export(Report report)
    {
        if (report == null) throw new ArgumentNullException(nameof(report));
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Report");
        // Header
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Range(1, 1, 1, 4).Merge().Style.Font.SetFontSize(16);
        worksheet.Cell(2, 1).Value = $"Period: {report.Range.Start: yyyy-MM-dd}-{report.Range.End: yyyy-MM-dd}";
        worksheet.Range(2, 1, 2, 4).Merge();
        // Table Header
        worksheet.Cell(4, 1).Value = "Item";
        worksheet.Cell(4, 2).Value = "Quantity";
        worksheet.Cell(4, 3).Value = "Price";
        worksheet.Cell(4, 4).Value = "Total";
        worksheet.Range(4, 1, 4, 4).Style.Font.SetBold();

        // Table Data
        int currentRow = 5;
        foreach (var row in report.Rows)
        {
            worksheet.Cell(currentRow, 1).Value = row.ItemName;
            worksheet.Cell(currentRow, 2).Value = row.Quantity;
            worksheet.Cell(currentRow, 3).Value = row.Price;
            worksheet.Cell(currentRow, 4).Value = row.Quantity * row.Price;
            currentRow++;
        }
        // Total Sum
        worksheet.Cell(currentRow, 3).Value = "Total";
        worksheet.Cell(currentRow, 4).Style.Font.SetBold();
        worksheet.Cell(currentRow, 4).FormulaA1 = $"=SUM(D5:D{currentRow - 1})";
        // Formatting
        worksheet.Columns().AdjustToContents();
        worksheet.Column(3).Style.NumberFormat.Format = "$#,##0.00";
        worksheet.Column(4).Style.NumberFormat.Format = "$#,##0.00";
        //Export 

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();

    }
}
