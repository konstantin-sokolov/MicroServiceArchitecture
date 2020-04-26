using Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess
{
    public sealed class IdentityContext :DbContext
    {
//        public IdentityContext()
//        {
//        }
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
        
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=identity;Username=postgres;Password={eqdpkjvftim123");
//        }
    }
}