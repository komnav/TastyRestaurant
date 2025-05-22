using System.Linq.Expressions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Tests.Integration;

public abstract class BaseTest
{
    private CustomWebApplicationFactory<Program> _factory;
    protected HttpClient _httpClient;
    
    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
        _factory.Dispose();
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