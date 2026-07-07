using FluentResults;
using Moq;
using SimpleNotes.Application.Services;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Tests.Services
{
    public class NoteServiceTests
    {
        [Fact]
        public async Task Given_CancellationToken_When_ListAsync_Then_ReturnNoteDomain()
        {
            // Arrange
            var noteEntities = EntityNote();

            var token = new CancellationToken();

            var noteRepository = new Mock<INoteRepository>();
            noteRepository
                .Setup(x => x.ListAsync(token))
                .ReturnsAsync(noteEntities);
            
            var userRepository = new Mock<IUserRepository>();

            var service = new NoteService(noteRepository.Object, userRepository.Object);

            // Act
            var result = await service.ListAsync(token);

            // Assert
            var okResult = Assert.IsType<Result<IEnumerable<NoteDomain>>>(result);

            Assert.Equal(result, okResult);
        }

        [Fact]
        public async Task Given_ValidId_When_GetAsync_Then_ReturnNoteDomain()
        {
            // Arrange
            var noteEntity = EntityNote();

            var noteDomain = DomainNote();

            var noteId = 1;

            var token = new CancellationToken();

            var userRepository = new Mock<IUserRepository>();

            var noteRepository = new Mock<INoteRepository>();
            noteRepository
                .Setup(x => x.GetAsync(noteId, token))
                .ReturnsAsync(noteEntity[0]);

            var service = new NoteService(noteRepository.Object, userRepository.Object);

            // Act
            var result = await service.GetAsync(noteId, token);

            // Assert
            var okResult = Assert.IsType<Result<NoteDomain>>(result);
            var resultValue = Assert.IsType<NoteDomain>(okResult.Value);

            Assert.Equal(noteDomain.Title.Value, resultValue.Title.Value);
            Assert.Equal(noteDomain.Content!.Value, resultValue.Content!.Value);
            Assert.Equal(noteDomain.UserId, resultValue.UserId);
        }

        [Fact]
        public async Task Given_ValidNoteDomain_When_CreateAsync_Then_ReturnOkNoteDomain()
        {
            // Arrange
            var noteEntity = EntityNote();
            var noteDomain = DomainNote();
            var userEntity = new UserEntity
                {
                    Id = 1
                };

            var token = new CancellationToken();

            var userRepository = new Mock<IUserRepository>();
            userRepository
                .Setup(x => x.GetUserAsync(userEntity.Id, token))
                .ReturnsAsync(userEntity);

            var noteRepository = new Mock<INoteRepository>();
            noteRepository
                .Setup(x => x.CreateAsync(
                    It.Is<NoteItemEntity>(n =>
                        n.Title == noteDomain.Title &&
                        n.Content == noteDomain.Content! &&
                        n.UserId == noteDomain.UserId),
                    token))
                .ReturnsAsync(noteEntity[0]);

            var service = new NoteService(noteRepository.Object, userRepository.Object);

            // Act
            var result = await service.CreateAsync(noteDomain, token);

            // Assert
            var okResult = Assert.IsType<Result<NoteDomain>>(result);
            var resultValue = Assert.IsType<NoteDomain>(okResult.Value);

            Assert.Equal(noteDomain.Title.Value, resultValue.Title.Value);
            Assert.Equal(noteDomain.Content!.Value, resultValue.Content!.Value);
            Assert.Equal(noteDomain.UserId, resultValue.UserId);
        }

        [Fact]
        public async Task Given_ValidNote_When_UpdateAsync_Then_ReturnTrue()
        {
            // Arrange
            var noteEntity = EntityNote();

            var updatedNote = new NoteItemEntity
            {
                Id = 1,
                Title = "Updated Title",
                Content = "Title was updated"
            };

            var noteDomain = new NoteDomain(
                AlphaText.Create(updatedNote.Title),
                1,
                AlphaText.Create(updatedNote.Content),
                updatedNote.Id);

            var token = new CancellationToken();

            var userRepository = new Mock<IUserRepository>();

            var noteRepository = new Mock<INoteRepository>();

            noteRepository
                .Setup(x => x.GetAsync(noteDomain.Id, token))
                .ReturnsAsync(updatedNote);

            noteRepository.Setup(x => x.UpdateAsync(It.Is<NoteItemEntity>(
                n => n.Title == updatedNote.Title &&
                n.Content == updatedNote.Content),
                token))
                .ReturnsAsync(true);

            var service = new NoteService(noteRepository.Object, userRepository.Object);

            // Act
            var result = await service.UpdateAsync(noteDomain, token);

            // Assert
            var okResult = Assert.IsType<Result<bool>>(result);

            Assert.True(okResult.Value);

            noteRepository.Verify(x => x.UpdateAsync(It.Is<NoteItemEntity>(n =>
                n.Title == updatedNote.Title &&
                n.Content! == updatedNote.Content),
                token),
                Times.Once);
        }

        [Fact]
        public async Task Give_ValidId_When_DeleteAsync_Then_ReturnTrue()
        {
            // Arrange
            var noteEntity = new NoteItemEntity
            {
                Id = 1
            };

            var token = new CancellationToken();

            var userRepository = new Mock<IUserRepository>();

            var noteRepository = new Mock<INoteRepository>();
            noteRepository
                .Setup(x => x.GetAsync(noteEntity.Id, token))
                .ReturnsAsync(noteEntity);
            noteRepository
                .Setup(x => x.DeleteAsync(1, token))
                .ReturnsAsync(true);

            var service = new NoteService(noteRepository.Object, userRepository.Object);

            // Act
            var result = await service.DeleteAsync(noteEntity.Id, token);

            // Assert
            var okResult = Assert.IsType<Result<bool>>(result);

            Assert.True(okResult.Value);
        }

        // Helper Function
        public static List<NoteItemEntity> EntityNote()
        {
            var notes = new List<NoteItemEntity>
            {
                new NoteItemEntity
                {
                    Id = 1,
                    Title = "Title One",
                    Content = "This is title one",
                    UserId = 1
                },
                new NoteItemEntity
                {
                    Id = 2,
                    Title = "Title Two",
                    Content = "This is title two",
                    UserId = 2
                }
            };

            return notes;
        }

        public static NoteDomain DomainNote()
        {
            var noteDomain = new NoteDomain(
                AlphaText.Create("Title One"),
                1,
                AlphaText.Create("This is title one"),
                1);

            return noteDomain;
        }
    }
}
