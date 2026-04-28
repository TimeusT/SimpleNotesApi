using SimpleNotes.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Application.DTOs;

public class CreateNoteRequest
{
    [MaxLength(25)]
    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Content { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;
}

public static class CreateNoteRequestExtension
{
    public static NoteDomain ToDomain(this CreateNoteRequest request)
    {
        return new NoteDomain(
            AlphaText.Create(request.Title),
            EmailText.Create(request.Email),
            AlphaText.Create(request.Content));
    }
}

public class UpdateNoteRequest
{
    [MaxLength(25)]
    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Content { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;
}

public static class UpdateNoteRequestExtension
{
    public static NoteDomain ToDomain(this UpdateNoteRequest request, string id)
    {
        return new NoteDomain(
            AlphaText.Create(request.Title),
            EmailText.Create(request.Email),
            AlphaText.Create(request.Content),
            id
        );
    }
}

public class NoteResponse
{
    public string UniqueId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public int UserId { get; set; }
}
