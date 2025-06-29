using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Tests.Integration;

public abstract class BaseTest
{
    private IntegrationTestWebAppFactory<Program> _factory;
    private DatabaseFixture databaseFixture = new();
    protected HttpClient HttpClient;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        await databaseFixture.Startasync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTurnDownSetup()
    {
        await databaseFixture.RestartAsync();
    }


    [SetUp]
    public void SetUp()
    {
        _factory = new IntegrationTestWebAppFactory<Program>(databaseFixture.GetConnectionString());
        HttpClient = _factory.CreateClient();
    }

    [TearDown]
    public async Task TearDown()
    {
        HttpClient.Dispose();
        await _factory.DisposeAsync();
        await databaseFixture.ResetDatabase();
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

    protected async Task LoginAsync(string userName, string password)
    {
        var loginRequest = new
        {
            email = userName,
            password = password
        };

        var responseMessage = await HttpClient.PostAsJsonAsync("/login", loginRequest);

        var dictionary = await responseMessage.Content.ReadFromJsonAsync<Dictionary<string, object>>();

        if (dictionary?.TryGetValue("accessToken", out object? token) == true)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }
    }
}