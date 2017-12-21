using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuestionApp.Models
{
    public partial class questiondbContext : DbContext
    {
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Questionnaire> Questionnaire { get; set; }
        public virtual DbSet<Reponse> Reponse { get; set; }
        public virtual DbSet<UtilisateurReponse> UtilisateurReponse { get; set; }

        public questiondbContext()
        {

        }

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
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.IdQuestions);

                entity.HasIndex(e => e.QuestionnaireIdQuestionnaire)
                    .HasName("IX_FK_QuestionnaireQuestion");

                entity.Property(e => e.QuestionnaireIdQuestionnaire).HasColumnName("Questionnaire_IdQuestionnaire");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.QuestionnaireIdQuestionnaireNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuestionnaireIdQuestionnaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireQuestion");
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(e => e.IdQuestionnaire);

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<Reponse>(entity =>
            {
                entity.HasKey(e => e.IdReponse);

                entity.HasIndex(e => e.QuestionReponseReponseIdQuestions)
                    .HasName("IX_FK_QuestionReponse");

                entity.Property(e => e.QuestionReponseReponseIdQuestions).HasColumnName("QuestionReponse_Reponse_IdQuestions");

                entity.Property(e => e.ValeurReponse).IsRequired();

                entity.HasOne(d => d.QuestionReponseReponseIdQuestionsNavigation)
                    .WithMany(p => p.Reponse)
                    .HasForeignKey(d => d.QuestionReponseReponseIdQuestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionReponse");
            });

            modelBuilder.Entity<UtilisateurReponse>(entity =>
            {
                entity.HasKey(e => e.IdUtilistaeurReponse);

                entity.HasIndex(e => e.AspNetUsersId)
                    .HasName("IX_FK_AspNetUsersUtilisateurReponse");

                entity.HasIndex(e => e.QuestionIdQuestions)
                    .HasName("IX_FK_QuestionUtilisateurReponse");

                entity.Property(e => e.AspNetUsersId)
                    .IsRequired()
                    .HasColumnName("AspNetUsers_Id");

                entity.Property(e => e.QuestionIdQuestions).HasColumnName("Question_IdQuestions");

                entity.HasOne(d => d.QuestionIdQuestionsNavigation)
                    .WithMany(p => p.UtilisateurReponse)
                    .HasForeignKey(d => d.QuestionIdQuestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionUtilisateurReponse");
            });
        }
    }
}
