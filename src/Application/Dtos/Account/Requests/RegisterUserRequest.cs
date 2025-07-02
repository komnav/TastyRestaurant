﻿namespace RestaurantLayer.Dtos.Account.Requests
{
    public class RegisterUserRequest
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
