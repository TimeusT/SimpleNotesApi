using Microsoft.EntityFrameworkCore;
using SimpleNotesApi.Repository.Entities;

namespace SimpleNotesApi.Repository
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
