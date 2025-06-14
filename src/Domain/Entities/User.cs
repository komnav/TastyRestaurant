using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public string Role { get; set; } = UserRoles.Customer;

    public int? ContactId { get; set; }

    public Contact? Contact { get; set; }
}