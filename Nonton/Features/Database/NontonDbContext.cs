using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Nonton.Features.Database
{
    public class NontonDbContext : DbContext
    {
        /// <summary>
        /// FIXME: This is required for EF Core 6.0 as it is not compatible with trimming.
        /// </summary>
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private static Type _keepDateOnly = typeof(DateOnly);

        public NontonDbContext(DbContextOptions<NontonDbContext> options) : base(options)
        {

        }

        public DbSet<NontonExtension> NontonExtensions { get; set; } = null!;
        public DbSet<DisabledExtension> DisabledExtensions { get; set; } = null!;
    }
}
