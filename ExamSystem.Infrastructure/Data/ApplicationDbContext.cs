using ExamSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ExamSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Students
            ConfigureStudents(modelBuilder);

            // Configure Admins
            ConfigureAdmins(modelBuilder);

            // Configure Subjects
            ConfigureSubjects(modelBuilder);

            // Configure Questions
            ConfigureQuestions(modelBuilder);

            // Configure Options
            ConfigureOptions(modelBuilder);

            // Configure StudentSubjects
            ConfigureStudentSubjects(modelBuilder);

            // Configure Exams
            ConfigureExams(modelBuilder);

            // Configure ExamQuestions
            ConfigureExamQuestions(modelBuilder);

            // Configure StudentAnswers
            ConfigureStudentAnswers(modelBuilder);

            // Configure ExamResults
            ConfigureExamResults(modelBuilder);
        }

        private void ConfigureStudents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                // Indexes
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.HasIndex(e => e.IsActive);
            });
        }

        private void ConfigureAdmins(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.AdminLevel)
                    .HasDefaultValue(1);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                // Indexes
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();
            });
        }

        private void ConfigureSubjects(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subjects");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.ExamDuration)
                    .IsRequired();

                entity.Property(e => e.TotalQuestionsCount)
                    .IsRequired();

                entity.Property(e => e.EasyQuestionsCount)
                    .IsRequired();

                entity.Property(e => e.NormalQuestionsCount)
                    .IsRequired();

                entity.Property(e => e.HardQuestionsCount)
                    .IsRequired();

                entity.Property(e => e.PassingScore)
                    .IsRequired();

                entity.Property(e => e.CreatedById)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                // Relationships
                entity.HasOne(e => e.CreatedBy)
                    .WithMany(e => e.Subjects)
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.IsActive);
                entity.HasIndex(e => e.CreatedById);
            });
        }

        private void ConfigureQuestions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(e => e.Difficulty)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreatedById)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                // Relationships
                entity.HasOne(e => e.Subject)
                    .WithMany(e => e.Questions)
                    .HasForeignKey(e => e.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedBy)
                    .WithMany(e => e.Questions)
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.SubjectId);
                entity.HasIndex(e => e.Difficulty);
                entity.HasIndex(e => e.IsActive);
                entity.HasIndex(e => e.CreatedById);

                // Check constraint
                entity.HasCheckConstraint("CK_Questions_Difficulty",
                    "Difficulty IN ('Easy', 'Normal', 'Hard')");
            });
        }

        private void ConfigureOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("Options");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.IsCorrect)
                    .IsRequired();

                entity.Property(e => e.Order)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Question)
                    .WithMany(e => e.Options)
                    .HasForeignKey(e => e.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Indexes
                entity.HasIndex(e => e.QuestionId);
                entity.HasIndex(e => e.IsCorrect);
            });
        }

        private void ConfigureStudentSubjects(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.ToTable("StudentSubjects");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.StudentId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.EnrollmentDate)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Student)
                    .WithMany(e => e.StudentSubjects)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Subject)
                    .WithMany(e => e.StudentSubjects)
                    .HasForeignKey(e => e.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.StudentId);
                entity.HasIndex(e => e.SubjectId);
                entity.HasIndex(e => new { e.StudentId, e.SubjectId })
                    .IsUnique();
            });
        }

        private void ConfigureExams(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exams");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.StudentId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.StartTime)
                    .IsRequired();

                entity.Property(e => e.Duration)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Student)
                    .WithMany(e => e.Exams)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                    .WithMany(e => e.Exams)
                    .HasForeignKey(e => e.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.StudentId);
                entity.HasIndex(e => e.SubjectId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.StartTime);

                // Check constraint
                entity.HasCheckConstraint("CK_Exams_Status",
                    "Status IN ('InProgress', 'Submitted', 'TimedOut', 'Evaluated')");
            });
        }

        private void ConfigureExamQuestions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.ToTable("ExamQuestions");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.ExamId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Order)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Exam)
                    .WithMany(e => e.ExamQuestions)
                    .HasForeignKey(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Question)
                    .WithMany(e => e.ExamQuestions)
                    .HasForeignKey(e => e.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.ExamId);
                entity.HasIndex(e => e.QuestionId);
                entity.HasIndex(e => new { e.ExamId, e.QuestionId })
                    .IsUnique();
            });
        }

        private void ConfigureStudentAnswers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.ToTable("StudentAnswers");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.ExamId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.SelectedOptionId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.AnsweredAt)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Exam)
                    .WithMany(e => e.StudentAnswers)
                    .HasForeignKey(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Question)
                    .WithMany(e => e.StudentAnswers)
                    .HasForeignKey(e => e.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SelectedOption)
                    .WithMany(e => e.StudentAnswers)
                    .HasForeignKey(e => e.SelectedOptionId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.ExamId);
                entity.HasIndex(e => e.QuestionId);
                entity.HasIndex(e => e.SelectedOptionId);
                entity.HasIndex(e => new { e.ExamId, e.QuestionId })
                    .IsUnique();
            });
        }

        private void ConfigureExamResults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.ToTable("ExamResults");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.ExamId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.StudentId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Score)
                    .IsRequired();

                entity.Property(e => e.TotalQuestions)
                    .IsRequired();

                entity.Property(e => e.PassingScore)
                    .IsRequired();

                entity.Property(e => e.IsPassed)
                    .IsRequired();

                entity.Property(e => e.EvaluatedAt)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Exam)
                    .WithOne(e => e.Result)
                    .HasForeignKey<ExamResult>(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.ExamId)
                    .IsUnique();
                entity.HasIndex(e => e.StudentId);
                entity.HasIndex(e => e.EvaluatedAt);
                entity.HasIndex(e => e.IsPassed);
            });
        }
    }
}
