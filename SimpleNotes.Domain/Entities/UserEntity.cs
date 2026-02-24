
namespace SimpleNotes.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public string? Email { get; set; }

    public DateTime JoinDate { get; set; }

    public ICollection<NoteItemEntity> Notes { get; set; } = null!; // Navigation (points to NoteItemEntity)
}
