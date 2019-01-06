using System;
using System.Collections.Generic;
using System.Text;
using Inky.Gamey.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inky.Gamey.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>().ToTable("Game");
            builder.Entity<Game>().HasMany(game => game.Sessions).WithOne(session => session.Game);
            builder.Entity<Game>().HasOne(game => game.CreatedByUser).WithMany(user => user.Games).HasForeignKey(game => game.CreatedBy);

            builder.Entity<Session>().ToTable("Session");
            builder.Entity<Session>().HasOne(session => session.CreatedByUser).WithMany(user => user.Sessions).HasForeignKey(session => session.CreatedBy);
        }
    }
}
