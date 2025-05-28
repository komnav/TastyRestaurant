using System.Linq.Expressions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Tests.Integration;

public abstract class BaseTest
{
    private IntegrationTestWebAppFactory<Program> _factory;
    protected HttpClient HttpClient;

    [SetUp]
    public async Task SetUp()
    {
        _factory = new IntegrationTestWebAppFactory<Program>();
        HttpClient = _factory.CreateClient();
        await _factory.InitializeAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        HttpClient.Dispose();
        await _factory.DisposeAsync();
    }

    protected async Task<T?> GetEntity<T>(Expression<Func<T, bool>> expression) where T : class
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
    }
}