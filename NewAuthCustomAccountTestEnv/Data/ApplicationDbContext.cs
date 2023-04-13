﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<NewAuthCustomAccountTestEnv.Models.UserModel> UserModel { get; set; } = default!;

        #endregion Properties

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
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

        #endregion Protected Methods
    }
}