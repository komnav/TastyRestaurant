using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Restaurant.WebApi.Extensions;
using Restaurant.WebApi.Middleware;
using RestaurantLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.JwtAuthServiceExtensions();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.AddServiceLayer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<DbInitilizer>();
initializer.Init();

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
app.Run();

public partial class Program
{
}