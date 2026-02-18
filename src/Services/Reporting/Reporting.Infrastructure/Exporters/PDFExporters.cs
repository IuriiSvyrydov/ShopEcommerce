

namespace Reporting.Infrastructure.Exporters;

public class PDFExporters : IReportExporter
{
    public ReportFormat Format => ReportFormat.PDF;

    public byte[] Export(Report report)
    {
        if (report == null) throw new ArgumentNullException(nameof(report));
        var pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Header()
                    .Text(report.Name)
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Black);
                page.Content()
                .Column(column =>
                {
                    column.Spacing(10);
                    column.Item().Text($"Report Period:{report.Range.Start: yyyy-MM-dd}-{report.Range}");
                    column.Item().Text($"Generated on:{DateTime.UtcNow: yyyy-MM-dd HH:mm}");
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();

                        });
                        table.Header(header =>
                        {
                            table.Cell().Text("Item").Bold();
                            table.Cell().Text("Quantity").Bold();
                            table.Cell().Text("Price").Bold();
                            table.Cell().Text("Total").Bold();
                        });
                        foreach (var row in report.Rows)
                        {
                            table.Cell().Text(row.ItemName);
                            table.Cell().Text(row.Quantity.ToString());
                            table.Cell().Text(row.Price.ToString("C"));
                            table.Cell().Text((row.Quantity * row.Price).ToString("C"));

                        }
                    });



                });
                page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });

            });
        }).GeneratePdf();
        return pdfBytes;
    }
}
