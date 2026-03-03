namespace SimpleNotes.Domain.Entities;

public class AddressEntity
{
    // PK AND FK
    public int Id { get; set; }

    public string StreetNo { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    // Linked user
    public UserEntity User { get; set; } = null!;
}
