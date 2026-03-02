namespace SimpleNotes.Application.Tests;

public class AddressDomain
{
    public string StreetNo { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int PostalCode { get; set; }
    public string Country { get; set; }

    public AddressDomain(
        string streetNo,
        string city,
        string state,
        int postalCode,
        string country)
    {
        StreetNo = streetNo;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
}
