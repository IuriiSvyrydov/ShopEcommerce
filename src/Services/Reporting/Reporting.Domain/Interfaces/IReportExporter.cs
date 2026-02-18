

using Reporting.Domain.Entities;
using Reporting.Domain.Enums;

namespace Reporting.Domain.Interfaces;

public interface IReportExporter
{
    public ReportFormat Format { get; }
    byte[] Export(Report report);
}
