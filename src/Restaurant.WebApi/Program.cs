using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Restaurant.WebApi.Extensions;
using Restaurant.WebApi.Middleware;
using RestaurantLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.JwtAuthServiceExtensions();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddAuthorizationWithIdentity();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitilizer>();
    initializer.Init();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<EfficientStopwatch>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<User>();

app.Run();

public partial class Program
{
}