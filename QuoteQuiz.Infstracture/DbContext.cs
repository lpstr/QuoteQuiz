using Microsoft.EntityFrameworkCore;
using QuoteQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.Infrastructure
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Quote> Quotes => Set<Quote>();
        public DbSet<GameSession> GameSessions => Set<GameSession>();
        public DbSet<GameQuestion> GameQuestions => Set<GameQuestion>();

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Username).IsRequired().HasMaxLength(100);
                b.Property(u => u.Email).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Author>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(a => a.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Quote>(b =>
            {
                b.HasKey(q => q.Id);
                b.Property(q => q.Text).IsRequired();

                b.HasOne(q => q.Author)
                    .WithMany(a => a.Quotes)
                    .HasForeignKey(q => q.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GameSession>(b =>
            {
                b.HasKey(gs => gs.Id);
                b.HasOne(gs => gs.User)
                    .WithMany(u => u.GameSessions)
                    .HasForeignKey(gs => gs.UserId);
            });

            modelBuilder.Entity<GameQuestion>(b =>
            {
                b.HasKey(gq => gq.Id);

                b.HasOne(gq => gq.GameSession)
                    .WithMany(gs => gs.Questions)
                    .HasForeignKey(gq => gq.GameSessionId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(gq => gq.Quote)
                    .WithMany(q => q.GameQuestions)
                    .HasForeignKey(gq => gq.QuoteId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(gq => gq.SuggestedAuthor)
                    .WithMany()
                    .HasForeignKey(gq => gq.SuggestedAuthorId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(gq => gq.SelectedAuthor)
                    .WithMany()
                    .HasForeignKey(gq => gq.SelectedAuthorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
        }
    }

}
