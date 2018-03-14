using Microsoft.EntityFrameworkCore;

namespace EFDebug.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Membership> Memberships { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
