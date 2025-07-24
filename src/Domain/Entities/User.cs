using System.Security.Cryptography.X509Certificates;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public int? ContactId { get; set; }

    public Contact? Contact { get; set; }
}