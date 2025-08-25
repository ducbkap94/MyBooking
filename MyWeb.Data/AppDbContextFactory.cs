using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyWeb.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<MyWebDbContext>
    {
        public MyWebDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyWebDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=BookingDb;User Id=SA;Password=Abc123456;TrustServerCertificate=True;");

            return new MyWebDbContext(optionsBuilder.Options);
        }
    }
}
