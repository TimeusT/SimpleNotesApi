using Microsoft.EntityFrameworkCore;
using SimpleNotes.Api.Repository.Entities;

namespace SimpleNotes.Api.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<NoteItemEntity> Notes { get; set; }
    }
}
