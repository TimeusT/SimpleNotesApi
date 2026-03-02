using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleNotes.Api.Controllers;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Domain;

namespace SimpleNotes.Api.Tests;

public class NoteControllerTests
{
    [Fact]
    public void Given_ValidCreateNoteRequest_When_CreateNote_Then_ReturnCreatedAtAction()
    {
        // Assign or Arrange
        // Mock the Service Interface
        var mockNoteService = new Mock<INoteService>();
        // Mock the create method
        var mockCreateNote = new NoteDomain(
            AlphaText.Create("Test"),
            1,
            AlphaText.Create("This is a test note"),
            1
            );
        // It.IsAny is a Moq feature that matches any 'NoteDomain' object passed to the 'Create' method
        mockNoteService.Setup(service => service.Create(It.IsAny<NoteDomain>())).Returns(mockCreateNote);

        // Create controller with the mocked service
        var controller = new NoteController(mockNoteService.Object);

        var validNoteRequest = new CreateNoteRequest
        {
            Title = "Test",
            Content = "Testing",
            UserId = 1
        };

        // Act
        var result = controller.Create(validNoteRequest);

        // Assert
        // Check for 201 response
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        // Check that the correct action was called
        Assert.Equal("GetById", createdAtActionResult.ActionName);
        // Ensure correct ID is passed
        Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
    }
}
