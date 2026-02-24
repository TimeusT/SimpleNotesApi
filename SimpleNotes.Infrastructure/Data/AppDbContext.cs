using Microsoft.EntityFrameworkCore;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public virtual DbSet<NoteItemEntity> Notes { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
}
