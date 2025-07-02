using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantLayer.Repositories;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextLayer(configuration);
        services.AddRepositoryLayer();
    }

    private static void AddDbContextLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbInitilizer>();
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
    }

    private static void AddRepositoryLayer(this IServiceCollection services)
    {
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}