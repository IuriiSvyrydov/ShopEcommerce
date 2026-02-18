

namespace Reporting.Domain.Exceptions;

public class ReportNotFoundException : ApplicationException
{
    public IReadOnlyCollection<ReportNotFoundItem> Errors { get; }
    public ReportNotFoundException(IEnumerable<ReportNotFoundItem> errors)
        : base("Report not found")
    {
        Errors = errors.ToList().AsReadOnly();
    }
    public static ReportNotFoundException WithId(string reportId)
        => new ReportNotFoundException(new[]
        {
            new ReportNotFoundItem(
                "ReportNotFound",
                $"Report with ID '{reportId}' was not found.")
        });
}
