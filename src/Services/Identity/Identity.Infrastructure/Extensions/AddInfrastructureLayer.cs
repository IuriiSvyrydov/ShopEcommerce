


using Identity.Application.Interfaces;
using Identity.Application.Settings.Email;
using Identity.Infrastructure.Interfaces;

namespace Identity.Infrastructure.Extensions;

public static class AddInfrastructureLayer
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            /*sql=>sql.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null)*/));

        //----------------------------------Services ----------------------------------/
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        //----------------------------------Repositories ----------------------------------/
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        //----------------------------------Settings ----------------------------------/
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;

    }
}
