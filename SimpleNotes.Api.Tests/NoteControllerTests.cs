using FluentResults;
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
    public async Task Given_CancellationToken_When_GetAllAsync_Then_ReturnOkValues()
    {
        // Arrange
        var allNotes = AllNotes();

        var token = new CancellationToken();

        var service = new Mock<INoteService>();
        service
            .Setup(s => s.ListAsync(token))
            .ReturnsAsync(allNotes);

        var controller = new NoteController(service.Object);

        // Act
        var result = await controller.ListAsync(token);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedNotes = Assert.IsType<List<NoteDomain>>(okResult.Value);

        service.Verify(s => s.ListAsync(token), Times.Once);

        Assert.Equal(allNotes.Count, returnedNotes.Count);

        Assert.Equal(allNotes[0].Title, returnedNotes[0].Title);
        Assert.Equal(allNotes[0].Content!, returnedNotes[0].Content);
        Assert.Equal(allNotes[0].UserId, returnedNotes[0].UserId);

        Assert.Equal(allNotes[1].Title, returnedNotes[1].Title);
        Assert.Equal(allNotes[1].Content!, returnedNotes[1].Content);
        Assert.Equal(allNotes[1].UserId, returnedNotes[1].UserId);

        Assert.Equal(allNotes, returnedNotes);
    }

    [Fact]
    public async Task Given_ValidId_When_GetAsync_Then_ReturnOkNoteResponse()
    {
        // Arrange
        var allNotes = AllNotes();

        var token = new CancellationToken();

        var service = new Mock<INoteService>();
        service.Setup(s => s.GetAsync(2, token))
            .ReturnsAsync(allNotes[1]);

        var controller = new NoteController(service.Object);

        // Act
        var result = await controller.GetAsync(2, token);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var noteResponse = Assert.IsType<NoteResponse>(okResult.Value);

        service.Verify(s => s.GetAsync(2, token), Times.Once);

        Assert.Equal(allNotes[1].Title, noteResponse.Title);
        Assert.Equal(allNotes[1].Content!, noteResponse.Content);
        Assert.Equal(allNotes[1].UserId, noteResponse.UserId);
    }

    [Fact]
    public async Task Given_ValidCreateNoteRequest_When_CreateNote_Then_ReturnCreatedAtAction()
    {
        // Arrange
        var validNote = new NoteDomain(
            AlphaText.Create("Test"),
            1,
            AlphaText.Create("Testing"),
            1
        );

        var token = new CancellationToken();

        var service = new Mock<INoteService>();

        service.Setup(x => x.CreateAsync(
            It.Is<NoteDomain>(n =>
                n.Title.Value == "Test" &&
                n.Content!.Value == "Testing" &&
                n.UserId == 1),
            token))
            .ReturnsAsync(validNote);

        var validNoteRequest = new CreateNoteRequest
        {
            Title = "Test",
            Content = "Testing",
            UserId = 1
        };

        var controller = new NoteController(service.Object);

        // Act
        var result = await controller.CreateAsync(validNoteRequest, token);

        // Assert
        var okResult = Assert.IsType<CreatedAtActionResult>(result);

        var okValue = (NoteResponse)okResult.Value!;

        Assert.Equal(validNote.Title, okValue.Title);
        Assert.Equal(validNote.Content!, okValue.Content);
        Assert.Equal(validNote.UserId, okValue.UserId);
    }

    [Fact]
    public async Task Given_NoteIdAndNoteItem_When_UpdateAsync_Then_ReturnOkNoteResponse()
    {
        // Arrange
        var noteId = 1;

        var updatedNote = new UpdateNoteRequest {
            Title = "Updated Title",
            Content = "Updated Content"
        };

        var token = new CancellationToken();

        var service = new Mock<INoteService>();
        service.Setup(s => s.UpdateAsync(
            It.Is<NoteDomain>(n =>
                n.Id == noteId &&
                n.Title.Value == updatedNote.Title &&
                n.Content!.Value == updatedNote.Content),
            token))
            .ReturnsAsync(Result.Ok(true));

        var controller = new NoteController(service.Object);

        // Act
        var result = await controller.UpdateAsync(noteId, updatedNote, token);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var okValue = Assert.IsType<NoteResponse>(okResult.Value);

        service.Verify(s => s.UpdateAsync(It.Is<NoteDomain>(
            n => n.Id == noteId &&
            n.Title.Value == updatedNote.Title &&
            n.Content!.Value == updatedNote.Content),
            token), Times.Once());

        Assert.Equal(updatedNote.Title, okValue.Title);
        Assert.Equal(updatedNote.Content, okValue.Content);
    }

    [Fact]
    public async Task Given_NoteId_When_DeleteAsync_Then_ReturnNoContent()
    {
        // Arrange
        var noteId = 1;

        var token = new CancellationToken();

        var service = new Mock<INoteService>();
        service.Setup(x => x.DeleteAsync(noteId, token))
            .ReturnsAsync(Result.Ok(true));

        var controller = new NoteController(service.Object);
        
        // Act
        var result = await controller.DeleteAsync(noteId, token);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);

        Assert.Equal(204, noContentResult.StatusCode);

        service.Verify(s => s.DeleteAsync(noteId, token), Times.Once());
    }

    // Helper Function
    public static List<NoteDomain> AllNotes()
    {
        var allNotes = new List<NoteDomain>
        {
            new NoteDomain(
                AlphaText.Create("Test one"),
                1,
                AlphaText.Create("This is a test note one"),
                1
            ),
            new NoteDomain(
                AlphaText.Create("Test two"),
                2,
                AlphaText.Create("This is a test note two"),
                2
            )
        };

        return allNotes;
    }
}
