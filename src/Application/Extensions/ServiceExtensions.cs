using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ResraurantLayer.Services;

namespace RestaurantLayer.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRolesService, RolesService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();
        builder.Services.AddScoped<IMenuItemService, MenuItemService>();
        builder.Services.AddScoped<ITableService, TableService>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
    }
}