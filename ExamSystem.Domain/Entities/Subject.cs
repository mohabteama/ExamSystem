using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Subject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExamDuration { get; set; }
        public int TotalQuestionsCount { get; set; }
        public int EasyQuestionsCount { get; set; }
        public int NormalQuestionsCount { get; set; }
        public int HardQuestionsCount { get; set; }
        public int PassingScore { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Admin CreatedBy { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
