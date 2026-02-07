

using Payment.Application.Interfaces;

namespace Payment.Infrastructure.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<PaymentDbContext>(opt=>opt.UseNpgsql(configuration.GetConnectionString("PaymentConnection")));
        /*-------------------Services-----------------------------------*/
            services.AddScoped<IPaymentService, PaymentService>();
            /*----------------Repositories=----------------------------*/
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            

            return services;
        }
    }
}
