using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ResraurantLayer.Services;

namespace Restaurant.WebApi.DI;

public static class ServiceExtensions
{
    public static void AddRepositoryLayer(this WebApplicationBuilder builder)
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

    public static void AddServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUpdateRolesService, UpdateRolesService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();
        builder.Services.AddScoped<IMenuItemService, MenuItemService>();
        builder.Services.AddScoped<ITableService, TableService>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
    }

    public static void AddDbContextLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbInitilizer>();
    }
    public static void AddDbContextToPostgres(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
    }
}