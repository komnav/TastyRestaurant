using System.Linq.Expressions;
using Domain.Entities;
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
        await _factory.InitializeAsync();
        HttpClient = _factory.CreateClient();
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

    protected async Task<T?> CreateEntity<T>(T entity) where T : class
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }

    protected async Task<int> CreateUser(Contact contact, User entity)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.Users.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}