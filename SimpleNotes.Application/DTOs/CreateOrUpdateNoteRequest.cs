using SimpleNotes.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Application.DTOs;

public class CreateNoteRequest
{
    [MaxLength(25)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Content { get; set; }

    public int UserId { get; set; }
}

public static class CreateNoteRequestExtension
{
    public static NoteDomain ToDomain(this CreateNoteRequest request)
    {
        return new NoteDomain(
            new AlphaText(request.Title),
            request.UserId,
            AlphaText.Create(request.Content));
    }
}

public class UpdateNoteRequest
{
    [MaxLength(25)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Content { get; set; }

    public int UserId { get; set; }
}

public static class UpdateNoteRequestExtension
{
    public static NoteDomain ToDomain(this UpdateNoteRequest request, int id)
    {
        return new NoteDomain(new AlphaText(request.Title), request.UserId, AlphaText.Create(request.Content), id);
    }
}

public class NoteResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public int UserId { get; set; }
}
