using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuestionApp.Models
{
    public partial class questiondbContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Questionnaire> Questionnaire { get; set; }
        public virtual DbSet<UserAnswer> UserAnswer { get; set; }

        public questiondbContext(DbContextOptions<questiondbContext> options)
                   : base(options)
        {
        }

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
