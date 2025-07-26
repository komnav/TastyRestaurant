using System.Reflection;
using Domain.Entities;
using Domain.Token;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Restaurant.WebApi.Extensions;
using Restaurant.WebApi.Middleware;
using Application.Extensions;
using Application.RwabbitMasstransit;
using MassTransit;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddOptions<JwtSettingOptions>().BindConfiguration(JwtSettingOptions.Section);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.AddAuthorizationWithIdentity();

// builder.Services.AddMassTransit(x =>
// {
//     x.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.Host("rabbitmq://localhost", h =>
//         {
//             h.Username("guest");
//             h.Password("guest");
//         });
//     });
// });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitilizer>();
    await initializer.Init();
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

namespace Restaurant.WebApi
{
    public partial class Program
    {
    }
}