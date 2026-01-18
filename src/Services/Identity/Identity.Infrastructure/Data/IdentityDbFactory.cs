

using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Infrastructure.Data;

    public sealed class IdentityDbFactory:IDesignTimeDbContextFactory<ApplicationUserDbContext>
    {
        public ApplicationUserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationUserDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IdentityDb;User Id=sa;Password=Passw0rd123!;TrustServerCertificate=True;Encrypt=False");
            return new ApplicationUserDbContext(optionsBuilder.Options);
        }
    }

