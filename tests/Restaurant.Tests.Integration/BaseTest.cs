using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.WebApi;

namespace Restaurant.Tests.Integration;

public abstract class BaseTest
{
    private IntegrationTestWebAppFactory<Program> _factory;
    private readonly DatabaseFixture _databaseFixture = new();
    protected HttpClient HttpClient;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        await _databaseFixture.Startasync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTurnDownSetup()
    {
        await _databaseFixture.RestartAsync();
    }


    [SetUp]
    public void SetUp()
    {
        _factory = new IntegrationTestWebAppFactory<Program>(_databaseFixture.GetConnectionString());
        HttpClient = _factory.CreateClient();
    }

    [TearDown]
    public async Task TearDown()
    {
        HttpClient.Dispose();
        await _factory.DisposeAsync();
        await _databaseFixture.ResetDatabase();
    }

    protected async Task<T?> GetEntity<T>(Expression<Func<T, bool>> expression) where T : class
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
    }

    protected async Task<string> GetRole(string role)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var adminRole = await roleManager.FindByNameAsync(role);
            if (adminRole != null) return role;
            else return null!;
        }
    }

    protected async Task<List<string?>> GetAllRole()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var roles = await roleManager.Roles.ToListAsync();
            return roles.Select(x => x.Name).ToList();
        }
    }

    protected async Task<List<string>> GetUserRoles(int idUser)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var getUser = await userManager.Users
                .FirstOrDefaultAsync(u => u.Id == idUser);

            var getRoles = await userManager.GetRolesAsync(getUser!);
            return getRoles.ToList();
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

    protected async Task<int> CreateUser(
        Contact contact,
        User entity,
        string password = "Admin1234$")
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = await userManager.CreateAsync(entity, password);

            if (result.Succeeded)
            {
                return 1;
            }

            return 0;
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

    protected async Task CreateRole(string role)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var newRole = new IdentityRole<int>(role);
            await roleManager.CreateAsync(newRole);
        }
    }
}