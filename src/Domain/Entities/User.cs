﻿namespace Domain.Entities;
public class User
{
    public int Id { get; set; }

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public required string Role { get; set; }

    public int? ContactId { get; set; }

    public Contact? Contact { get; set; }
}
