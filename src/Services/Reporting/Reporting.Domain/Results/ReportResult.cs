

namespace Reporting.Domain.Results;

public class ReportResult
{
    public byte[] Content { get; set; }
    public string FileName { get; set; }
    public string  ContentType { get; set; }
    public long Size=> Content.Length;
    public DateTime GeneratedAt { get; }

    public ReportResult(byte[] content, string fileName, string contentType)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType;
        GeneratedAt = DateTime.UtcNow;
    }
}
