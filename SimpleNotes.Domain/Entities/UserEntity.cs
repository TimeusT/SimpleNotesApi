
namespace SimpleNotes.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string FName { get; set; }

    public string LName { get; set; }

    public int Age { get; set; }

    public string? Email { get; set; }

    public DateTime JoinDate { get; set; }
}
