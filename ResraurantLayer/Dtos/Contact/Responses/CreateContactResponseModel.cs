using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.Contact.Responses;

public record CreateContactResponseModel(
     int Id,
     [Required] string FirstName,
     string? LastName,
     string? PassportSeries,
     string? Email,
     string? PhoneNumber,
     string? Address
     );
