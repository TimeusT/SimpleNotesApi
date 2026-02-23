
namespace SimpleNotes.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string FName { get; set; }

    public string LName { get; set; }

    public int Age { get; set; }

    public EmailText? Email { get; set; }

    public DateTime JoinDate { get; set; }
}
