




namespace Reporting.Infrastructure.Extensions;

public static class AddInfrastructureLayer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services, e.g., exporters, repositories, etc.
        services.AddScoped<IReportExporter, PDFExporters>();
        services.AddScoped<IReportExporter, ExcelExporter>();
        // Add other infrastructure services as needed
        return services;
    }
}
