using Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess
{
    public sealed class IdentityContext :DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.UserName)
                .IsRequired();
        }
    }
}