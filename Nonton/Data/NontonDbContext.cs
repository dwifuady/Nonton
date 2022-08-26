using Microsoft.EntityFrameworkCore;

namespace Nonton.Data
{
    public class NontonDbContext : DbContext
    {
        public NontonDbContext(DbContextOptions<NontonDbContext> options): base(options)
        {
            
        }

        public DbSet<NontonExtension> NontonExtensions { get; set; } = null!;
    }
}
