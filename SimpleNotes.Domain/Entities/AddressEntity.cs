namespace SimpleNotes.Domain.Entities;

public class AddressEntity
{
    public string StreetNo { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int PostCode { get; set; }

    public string Country { get; set; } = string.Empty;

    // Foreign key
    public int UserId { get; set; }

    // Linked user
    public UserEntity User { get; set; } = null!;
}
