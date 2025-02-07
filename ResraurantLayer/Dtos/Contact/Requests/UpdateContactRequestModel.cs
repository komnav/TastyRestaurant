using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.Contact.Requests;

public record UpdateContactRequestModel(
    [Required] string FirstName,
    string LastName,
    string PassportSeries,
    string Email,
    string PhoneNumber,
    string Address
    );