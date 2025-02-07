using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.Contact.Responses;

public record GetContactResponseModel(
    int id,
    [Required] string FirstName,
    string? LastName,
    string? PassportSeries,
    string? Email,
    string? PhoneNumber,
    string? Address
    );
