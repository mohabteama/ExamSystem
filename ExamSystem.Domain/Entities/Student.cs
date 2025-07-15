using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Exam> Exams { get; set; }
        //public ICollection<Subject> Subjects { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
