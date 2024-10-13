using AccessPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessPoint.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Users> User { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships management on entities/tables creation in database
            modelBuilder.Entity<Users>()
                .HasMany(u => u.LoginHistory)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<Users>()
                .Property(u => u.CurrentBalance)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
