using System.Data.Common;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Tests.Integration;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == 
                     typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase("In memorytest");
            });
            
            var initilizer = services.SingleOrDefault(
                d => d.ServiceType == 
                     typeof(DbInitilizer));

            services.Remove(initilizer);
            
            services.AddScoped<DbInitilizer>(s =>
            {
                var service = ActivatorUtilities.CreateInstance<DbInitilizer>(s);
                service.SupportMigration = false;
                return service;
            });
        });

        builder.UseEnvironment("Development");
    }
}