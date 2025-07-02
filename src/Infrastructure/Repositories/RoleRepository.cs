using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantLayer.Repositories;

namespace Infrastructure.Repositories;

public class RoleRepository(
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager)
    : IRoleRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

    public async Task<int> AddToRoleAsync(string userName, string role)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
        if (getUser is null)
            throw new Exception("User not found");

        var getRole = await FindRoleIfDoesntExistAdd(role, getUser);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (getRole is null) return 0;
        return 1;
    }

    public async Task<int> DeleteAsync(string userName, string roles)
    {
        return await _userManager.Users
            .Where(u => u.UserName == userName)
            .ExecuteDeleteAsync();
    }

    public async Task<int> AddRoleAsync(string role)
    {
        var adminRole = await _roleManager.FindByNameAsync(role);
        if (adminRole == null)
        {
            var newRole = new IdentityRole<int>(role);
            await _roleManager.CreateAsync(newRole);
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public async Task<User?> GetRoleUserAsync(string userName)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public Task<List<User>> GetAllUserAsync(string userName)
    {
        throw new NotImplementedException();
    }

    private async Task<string> FindRoleIfDoesntExistAdd(string roles, User user)
    {
        var getRole = FindRole(roles);
        var addAndReturn = AddToRole(getRole, user);
        return await addAndReturn;
    }

    private string FindRole(string roles)
    {
        switch (roles)
        {
            case "admin": return UserRoles.Admin;
            case "SuperAdmin": return UserRoles.SuperAdmin;
            case "Cooker": return UserRoles.Cooker;
            case "Cashier": return UserRoles.Cashier;
            case "Waiter": return UserRoles.Waiter;
            default: return UserRoles.Customer;
        }
    }

    private async Task<string> AddToRole(string role, User user)
    {
        var adminRole = await _roleManager.FindByNameAsync(role);
        if (adminRole == null) throw new Exception($"This {role} doesn't exist");

        await _userManager.AddToRoleAsync(user, $"{role}");
        return role;
    }
}