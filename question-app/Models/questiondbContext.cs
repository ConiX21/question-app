using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace question_app.Models
{
    public partial class questiondbContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Questionnaire> Questionnaire { get; set; }
        public virtual DbSet<UserAnswer> UserAnswer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=questiondb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.IdAnswer);

                entity.HasIndex(e => e.QuestionIdQuestions)
                    .HasName("IX_FK_QuestionAnswer");

                entity.Property(e => e.AnswerText)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.QuestionIdQuestionsNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionIdQuestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionAnswer");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.IdQuestions);

                entity.HasIndex(e => e.QuestionnaireIdQuestionnaire)
                    .HasName("IX_FK_QuestionnaireQuestion");

                entity.Property(e => e.QuestionText).IsUnicode(false);

                entity.HasOne(d => d.QuestionnaireIdQuestionnaireNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuestionnaireIdQuestionnaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireQuestion");
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(e => e.IdQuestionnaire);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => e.IdUserAnswer);

                entity.HasIndex(e => e.AspNetUsersId)
                    .HasName("IX_FK_AspNetUsersUserAnswer");

                entity.HasIndex(e => e.QuestionIdQuestions)
                    .HasName("IX_FK_UserAnswerQuestion");

                entity.HasIndex(e => e.QuestionnaireIdQuestionnaire)
                    .HasName("IX_FK_UserAnswerQuestionnaire");

                entity.Property(e => e.AspNetUsersId).IsRequired();

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.AspNetUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsersUserAnswer");

                entity.HasOne(d => d.QuestionIdQuestionsNavigation)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.QuestionIdQuestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAnswerQuestion");

                entity.HasOne(d => d.QuestionnaireIdQuestionnaireNavigation)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.QuestionnaireIdQuestionnaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAnswerQuestionnaire");
            });
        }
    }
}
