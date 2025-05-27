using Infrastructure;
using Infrastructure.Repositories;
using ResraurantLayer.Services;
using Restaurant.WebApi.Middleware;
using Application.Dtos;
using Application.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

builder.AddJwtTokenService();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.AddDbContextToPostgres();

builder.AddRepositoryLayer();
builder.AddServiceLayer();
builder.AddDbContextLayer();

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