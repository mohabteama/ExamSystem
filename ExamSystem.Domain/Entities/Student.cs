using Microsoft.AspNetCore.Identity;

namespace ExamSystem.Domain.Entities
{
    public class Student : IdentityUser
    {
        public bool IsActive { get; set; } = true;

        public ICollection<Exam> Exams { get; set; }
        //public ICollection<Subject> Subjects { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
