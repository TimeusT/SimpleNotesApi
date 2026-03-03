namespace SimpleNotes.Domain;

public class UserDomain
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public DateTime JoinDate { get; private set; }
    public EmailText? Email { get; private set; }
    public AddressDomain? Address { get; private set; }

    public UserDomain(
        string fName,
        string lName,
        int age,
        DateTime joinDate,
        int? id = default,
        EmailText? email = default,
        AddressDomain? address = default
        )
    {
        FirstName = fName;
        LastName = lName;
        Age = age;
        JoinDate = joinDate;
        Id = id ?? 0;
        Email = email;
        Address = address;
    }
}
