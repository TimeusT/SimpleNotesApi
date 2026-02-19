using Microsoft.EntityFrameworkCore;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public DbSet<NoteItemEntity> Notes { get; set; }
}
