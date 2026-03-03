using SimpleNotes.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Application.DTOs;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? Email { get; set; }
}

public class CreateUserRequest
{
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public DateTime JoinDate { get; set; }

    [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "The field {0} should follow email format.")]
    public string? Email { get; set; }

    public UserAddress? UserAddress { get; set; }
}


public class UserAddress
{
    [Required]
    public string StreetNo { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string State { get; set; } = string.Empty;
    [Required]
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    public string Country { get; set; } = string.Empty;
}


public static class CreateUserRequestExtension
{
    public static UserDomain ToDomain(this CreateUserRequest request)
    {
        return new UserDomain
        (
            request.FirstName,
            request.LastName,
            request.Age,
            request.JoinDate,
            email: EmailText.Create(request.Email),
            address: request.ToAddressDomain()
        );
    }

    public static AddressDomain? ToAddressDomain(this CreateUserRequest request)
    {
        if (request.UserAddress == null) return null;

        return new AddressDomain(
            request.UserAddress.StreetNo,
            request.UserAddress.City,
            request.UserAddress.State,
            request.UserAddress.PostalCode,
            request.UserAddress.Country);
    }
}



public class UpdateUserRequest
{
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters.")]

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public DateTime JoinDate { get; set; }

    [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "The field {0} should follow email format.")]
    public string? Email { get; set; }
}

public static class UpdateUserRequestExtension
{
    public static UserDomain ToDomain(this UpdateUserRequest request, int id)
    {
        return new UserDomain
        (
            request.FirstName,
            request.LastName,
            request.Age,
            request.JoinDate,
            id,
            email: EmailText.Create(request.Email)
        );
    }
}