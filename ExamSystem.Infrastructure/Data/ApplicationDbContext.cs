using ExamSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                    
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(s => s.Exams)
                      .WithOne(e => e.Student)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.StudentSubjects)
                      .WithOne(ss => ss.Student)
                      .HasForeignKey(ss => ss.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.StudentAnswers)
                      .WithOne(ss => ss.Student)
                      .HasForeignKey(ss => ss.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(s => s.Questions)
                      .WithOne(q => q.Subject)
                      .HasForeignKey(q => q.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.Exams)
                      .WithOne(e => e.Subject)
                      .HasForeignKey(e => e.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.StudentSubjects)
                      .WithOne(ss => ss.Subject)
                      .HasForeignKey(ss => ss.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(q => q.Options)
                      .WithOne(o => o.Question)
                      .HasForeignKey(o => o.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(q => q.ExamQuestions)
                      .WithOne(eq => eq.Question)
                      .HasForeignKey(eq => eq.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(q => q.StudentAnswers)
                      .WithOne(sa => sa.Question)
                      .HasForeignKey(sa => sa.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(o => o.StudentAnswers)
                      .WithOne(sa => sa.SelectedOption)
                      .HasForeignKey(sa => sa.SelectedOptionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasMany(e => e.ExamQuestions)
                      .WithOne(eq => eq.Exam)
                      .HasForeignKey(eq => eq.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.StudentAnswers)
                      .WithOne(sa => sa.Exam)
                      .HasForeignKey(sa => sa.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Result)
                      .WithOne(er => er.Exam)
                      .HasForeignKey<ExamResult>(er => er.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(er => er.Student)
                      .WithMany()
                      .HasForeignKey(er => er.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}