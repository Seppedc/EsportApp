using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace EsportApp.api.Entities
{
    public class EsportAppContext: IdentityDbContext<
        User,
        Role,
        Guid,
        IdentityUserClaim<Guid>,
        UserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameTitle> GameTitles { get; set; }
        public DbSet<GameTitleTeam> GameTitleTeams { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamGame> TeamGames { get; set; }
        public DbSet<Tornooi> Tornooien { get; set; }
        //public DbSet<TornooiGame> TornooiGames { get; set; }
        public DbSet<TornooiTeam> TornooiTeams { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<UserGameTitle> UserGameTitles { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        public EsportAppContext(DbContextOptions<EsportAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null) { throw new ArgumentNullException(nameof(modelBuilder)); }

            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
               .WithOne(e => e.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
            });




                

            modelBuilder.Entity<UserGameTitle>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.UserGameTitles)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.GameTitle)
                .WithMany(x => x.UserGameTitles)
                .HasForeignKey(x => x.GameTitleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserTeam>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.UserTeams)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.Team)
                .WithMany(x => x.UserTeams)
                .HasForeignKey(x => x.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserGame>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.UserGames)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.Game)
                .WithMany(x => x.UserGames)
                .HasForeignKey(x => x.GameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GameTitleTeam>(x =>
            {
                x.HasOne(x => x.GameTitle)
                .WithMany(x => x.GameTitleTeams)
                .HasForeignKey(x => x.GameTitleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.Team)
                .WithMany(x => x.GameTitleTeams)
                .HasForeignKey(x => x.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TeamGame>(x =>
            {
                x.HasOne(x => x.Team)
                .WithMany(x => x.TeamGames)
                .HasForeignKey(x => x.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.Game)
                .WithMany(x => x.TeamGames)
                .HasForeignKey(x => x.GameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TornooiTeam>(x =>
            {
                x.HasOne(x => x.Toernooi)
                .WithMany(x => x.TornooiTeams)
                .HasForeignKey(x => x.ToernooiId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(x => x.Team)
                .WithMany(x => x.ToernooiTeams)
                .HasForeignKey(x => x.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            //modelBuilder.Entity<TornooiGame>(x =>
            //{
            //    x.HasOne(x => x.Toernooi)
            //    .WithMany(x => x.TornooiGames)
            //    .HasForeignKey(x => x.ToernooiId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //    x.HasOne(x => x.Game)
            //    .WithMany(x => x.TornooiGames)
            //    .HasForeignKey(x => x.GameId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
            //});

            modelBuilder.Entity<Tornooi>(x =>
            {
                x.HasOne(x => x.GameTitle)
                .WithMany(x => x.Toernooien)
                .HasForeignKey(x => x.GameTitleId)
                .IsRequired();
            });
            modelBuilder.Entity<Game>(x =>
            {
                x.HasOne(x => x.Tornooi)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.ToernooiId)
                .IsRequired();
            });
            //Indexes

            modelBuilder.Entity<User>(x =>
            {
                x.HasIndex(x => new { x.Email })
                .IsUnique(true)
                .HasDatabaseName("UQ_User_Email");
            });
        }
    }
}
