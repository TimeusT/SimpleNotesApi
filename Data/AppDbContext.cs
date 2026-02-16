using Microsoft.EntityFrameworkCore;
using SimpleNotesApi.Models;

namespace SimpleNotesApi.Data
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
