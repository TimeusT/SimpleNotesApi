using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Repositories;

namespace SimpleNotes.Infrastructure.Tests
{
    public class NoteRepositoryTests
    {
        //[Fact]
        //public void Given_ValidId_When_NoteExists_Then_ReturnNote()
        //{
        //    // Arrange
        //    var validId = 1;
        //    var expectedNote = new NoteItemEntity { Id = validId, Title = "TestTitle", Content = "TestContent" };

        //    var data = new List<NoteItemEntity> { expectedNote }.AsQueryable();

        //    var mockDbSet = new Mock<DbSet<NoteItemEntity>>();
        //    mockDbSet.As<IQueryable<NoteItemEntity>>()
        //             .Setup(m => m.Provider).Returns(data.Provider);
        //    mockDbSet.As<IQueryable<NoteItemEntity>>()
        //             .Setup(m => m.Expression).Returns(data.Expression);
        //    mockDbSet.As<IQueryable<NoteItemEntity>>()
        //             .Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockDbSet.As<IQueryable<NoteItemEntity>>()
        //             .Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        //    var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        //    mockDbContext.Setup(x => x.Notes).Returns(mockDbSet.Object);

        //    // Act
        //    var sut = new NoteRepository(mockDbContext.Object);
        //    var note = sut.Get(validId);

        //    // Assert
        //    Assert.NotNull(note);  // Ensure that a note is returned
        //    Assert.Equal(expectedNote.Id, note.Id);  // Compare individual properties
        //    Assert.Equal(expectedNote.Title, note.Title);
        //    Assert.Equal(expectedNote.Content, note.Content);
        //}

        [Fact]
        public async Task Given_ValidId_When_NoteExists_Then_ReturnNote()
        {
            // Arrange
            var expectedNote = new NoteItemEntity
            {
                Id = 1,
                Title = "TestTitle",
                Content = "TestContent"
            };

            var data = new List<NoteItemEntity> { expectedNote }.AsQueryable();

            var mockDbSet = new Mock<DbSet<NoteItemEntity>>();
            mockDbSet.As<IQueryable<NoteItemEntity>>()
                     .Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<NoteItemEntity>>()
                     .Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<NoteItemEntity>>()
                     .Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<NoteItemEntity>>()
                     .Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            mockDbContext
                .Setup(x => x.Notes)
                .Returns(mockDbSet.Object);

            var token = new CancellationToken();

            // Act
            var repository = new NoteRepository(mockDbContext.Object);
            var note = await repository.GetAsync(expectedNote.Id, token);

            // Assert
            Assert.NotNull(note);  // Ensure that a note is returned
            Assert.Equal(expectedNote.Id, note.Id);  // Compare individual properties
            Assert.Equal(expectedNote.Title, note.Title);
            Assert.Equal(expectedNote.Content, note.Content);
        }
    }
}
