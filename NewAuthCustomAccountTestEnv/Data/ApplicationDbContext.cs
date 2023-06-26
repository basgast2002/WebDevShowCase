using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewAuthCustomAccountTestEnv.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Public Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<Models.BadgeModel> Badge { get; set; } = default!;
        /*
        public DbSet<Models.UserBadges> UserBadges { get; set; } = default!;
        public DbSet<Models.UserModel> UserModel { get; set; } = default!;
        */

        #endregion Properties

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<UserBadges>().HasKey(ub => new { ub.UserId, ub.BadgeId });

            builder.Entity<ApplicationUser>(user =>
            {
                user.Property(u => u.Name).IsRequired().HasMaxLength(50);
                user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                user.Property(u => u.Email).IsRequired().HasMaxLength(50);
            });
            /*

            builder.Entity<BadgeModel>(badge =>
            {
                badge.Property(b => b.Id).IsRequired();
                badge.Property(b => b.Name).IsRequired();
                badge.Property(b => b.UnlockedAt).IsRequired().HasDefaultValue(100);
                badge.Property(b => b.ImageUrl).IsRequired().HasDefaultValue();
            });
            */
        }

        #endregion Protected Methods
    }
}