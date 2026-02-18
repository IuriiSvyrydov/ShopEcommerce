
using Reporting.Domain.Entities;
using Reporting.Domain.Enums;
using Reporting.Domain.Results;

namespace Reporting.Domain.Interfaces;

public interface IReportGenerator
{
    Task<ReportResult> GenerateReportAsync(Report report, CancellationToken ctx);
    bool SupportsFormat(ReportFormat format);
}
