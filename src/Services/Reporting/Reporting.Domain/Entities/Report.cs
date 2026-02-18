
using Reporting.Domain.Enums;
using Reporting.Domain.Exceptions;
using Reporting.Domain.ValueObjects;

namespace Reporting.Domain.Entities;

public sealed class Report
{
    #region Properties
    public Guid Id { get; private set; }
    public string Name { get;  private set; } = string.Empty;
    public DateRange Range { get; private set; }
    public ReportFormat Format { get; private set; }
    public bool IsGenerated  { get; private set; }

    public byte[]? FileContent { get; private set; }
    public DateTime? CreateOn { get; private set; } = DateTime.UtcNow;
    public string? FilePath { get; private set; }
    private readonly List<ReportRow> _rows = new();
    public IReadOnlyCollection<ReportRow> Rows => _rows.AsReadOnly();

    private readonly List<string> _notes = new();
    public IReadOnlyCollection<string> Notes => _notes.AsReadOnly();
    #endregion

    public Report(Guid id ,DateRange range, ReportFormat format)
    {
        Id = id;
        Range = range;
        Format = format;
        IsGenerated = false;
    }
    #region Methods
    //simulate report generation
    public void AddRow(ReportRow row) => _rows.Add(row);
    public void AddRows(IEnumerable<ReportRow> rows) => _rows.AddRange(rows);
    public void GenerateReport(string filePath)
    {
        if(string.IsNullOrWhiteSpace(filePath))
            throw new ReportNotFoundException(new[]
            {
                new ReportNotFoundItem(
                    "InvalidFilePath",
                    "The provided file path is invalid.")
            });
        FilePath = filePath;
        IsGenerated = true;
        CreateOn = DateTime.UtcNow;
        AddNotes($"Report generated at {CreateOn.Value.ToString("u")} with format {Format} for range {Range.Start:u} to {Range.End:u}.");

    }
    public void AddNotes(string note)
    {
        if(string.IsNullOrWhiteSpace(note))
            throw new ReportNotFoundException(new[]
            {
                new ReportNotFoundItem(
                    "InvalidNote",
                    "The provided note is invalid.")
            });
        _notes.Add(note);
    }
    public void UpdateRange(DateRange newRange)
    {
        Range = newRange;
        AddNotes($"Report range updated to {Range.Start:yyyy-MM-dd} - {Range.End:yyyy-MM-dd}.");
    }
    public void UpdateFormat(ReportFormat newFormat)
    {
        Format = newFormat;
        AddNotes($"Report format updated to {Format}.");
    }
    #endregion
}