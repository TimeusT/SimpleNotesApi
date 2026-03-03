using SimpleNotes.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Application.DTOs;

public class AddressResponse
{
    public string StreetNo { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;
}

public class CreateAddressRequest
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string StreetNo { get; set; } = string.Empty;

    [MaxLength(250)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string City { get; set; } = string.Empty;

    [MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string State { get; set; } = string.Empty;

    [MaxLength(4)]
    [RegularExpression(@"^\d+$", ErrorMessage = "The field {0} can only contain numbers.")]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string Country { get; set; } = string.Empty;
}

public static class CreateAddressRequestExtension
{
    public static AddressDomain ToDomain(this CreateAddressRequest request)
    {
        return new AddressDomain
        (
            request.StreetNo,
            request.City,
            request.State,
            request.PostalCode,
            request.Country
        );
    }
}

public class UpdateAddressRequest
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string StreetNo { get; set; } = string.Empty;

    [MaxLength(250)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string City { get; set; } = string.Empty;

    [MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string State { get; set; } = string.Empty;

    [MaxLength(4)]
    [RegularExpression(@"^\d+$", ErrorMessage = "The field {0} can only contain numbers.")]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string Country { get; set; } = string.Empty;
}

public static class UpdateAddressRequestExtension
{
    public static AddressDomain ToDomain(this UpdateAddressRequest request, int id)
    {
        return new AddressDomain
        (
            request.StreetNo,
            request.City,
            request.State,
            request.PostalCode,
            request.Country
        );
    }
}