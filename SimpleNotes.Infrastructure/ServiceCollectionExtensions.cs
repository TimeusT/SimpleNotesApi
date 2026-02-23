using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Infrastructure.Data;

namespace SimpleNotes.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNoteDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer
            (
                configuration.GetConnectionString("DefaultConnection")
            ));
            return services;
        }
    }
}
