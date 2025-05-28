using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.AddDbContextToPostgres();
        builder.AddDbContextLayer();
        builder.AddRepositoryLayer();
    }

    private static void AddDbContextLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbInitilizer>();
    }

    private static void AddDbContextToPostgres(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
    }

    private static void AddRepositoryLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        builder.Services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IUpdateRolesRepository, UpdateRolesRepository>();
    }
}