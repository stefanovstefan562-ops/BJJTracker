using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyMvcApp.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=localhost,1433;Database=BjjTrackerDb;User Id=sa;Password=Ste5971114fan;TrustServerCertificate=True;");

            return new ApplicationDbContext(builder.Options);
        }
    }
}