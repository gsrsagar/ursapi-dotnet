using Microsoft.EntityFrameworkCore;
using urs_api.Models;
using urs_api.Models.Implementaions;

namespace urs_api.DbContexts
{
    public class URSDbContext:DbContext
    {
            public URSDbContext()
            {
            }

            public URSDbContext(DbContextOptions<URSDbContext> options)
                : base(options)
            {
            }

            public DbSet<PastProgram> PastProgram { get; set; }
            public DbSet<Location> Location { get; set; }
            public DbSet<User> User { get; set; }
            public DbSet<Attachments> Attachments { get; set; }
            public DbSet<ContactUs> ContactUs { get; set; }
    }
}
