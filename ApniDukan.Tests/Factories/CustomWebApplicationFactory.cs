
using ApniDukan.Infrastrcture.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Data.Sqlite;

namespace ApniDukan.Tests.Factories
{
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection;

        public CustomWebApplicationFactory()
        {
            SQLitePCL.Batteries.Init();

            _connection = new SqliteConnection(
                "Data Source=:memory:");

            _connection.Open();
        }

        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext registrations
                services.RemoveAll<AppDbContext>();

                // Remove existing DbContext options
                services.RemoveAll<DbContextOptions<AppDbContext>>();

                // IMPORTANT:
                // Remove existing EF Core provider configurations
                services.RemoveAll<
                    IDbContextOptionsConfiguration<AppDbContext>>();

                // Register SQLite for integration testing
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _connection.Dispose();
            }
        }
    }
}
