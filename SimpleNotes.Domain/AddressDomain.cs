namespace SimpleNotes.Domain;

public class AddressDomain
{
    public int Id { get; set; }

    public string StreetNo { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public AddressDomain(
        string streetNo,
        string city,
        string state,
        string postalCode,
        string country,
        int? id = default
        )
    {
        Id = id ?? 0;
        StreetNo = streetNo;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
}
