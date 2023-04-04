using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewAuthCustomAccountTestEnv.Models;

namespace NewAuthCustomAccountTestEnv.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(user =>
            {
                user.Property(u => u.Name).IsRequired().HasMaxLength(50);
                user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                user.Property(u => u.Email).IsRequired().HasMaxLength(50);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<NewAuthCustomAccountTestEnv.Models.UserModel> UserModel { get; set; } = default!;


    }
}