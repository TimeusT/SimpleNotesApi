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
    public virtual DbSet<AddressEntity> Address { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<AddressEntity>(a => a.Id);
    }
}
