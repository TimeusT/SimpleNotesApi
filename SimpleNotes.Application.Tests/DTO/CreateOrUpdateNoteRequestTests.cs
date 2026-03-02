using SimpleNotes.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Application.Tests.DTO;

public class CreateOrUpdateNoteRequestTests
{
    [Fact]
    public void Given_InvalidCreateNoteRequest_When_CreateNote_Then_ReturnErrorMessage()
    {
        // Assign
        var invalidNote = new CreateNoteRequest
        {
            Title = "Invalid Title !",
            Content = "This is a test note",
            UserId = 1
        };

        var validationResult = new List<ValidationResult>();
        var context = new ValidationContext(invalidNote);

        // Act
        var isValid = Validator.TryValidateObject(
            invalidNote,
            context,
            validationResult,
            validateAllProperties: true
            );

        // Assert
        Assert.False(isValid); // invalidNote should be invalid
        Assert.Contains(validationResult, v => v.ErrorMessage.Contains("The field Title can only contain letters and spaces."));
    }

    [Fact]
    public void Given_InvalidUpdateNoteRequest_When_UpdateNote_Then_ReturnErrorMessage()
    {
        // Assign
        var invalidNote = new UpdateNoteRequest
        {
            Title = "Invalid Title !",
            Content = "This is a test note",
            UserId = 1
        };

        var validationResult = new List<ValidationResult>();
        var context = new ValidationContext(invalidNote);

        // Act
        var isValid = Validator.TryValidateObject(
            invalidNote,
            context,
            validationResult,
            validateAllProperties: true
            );

        // Assert
        Assert.False(isValid); // invalidNote should be invalid
        Assert.Contains(validationResult, v => v.ErrorMessage.Contains("The field Title can only contain letters and spaces."));
    }
}