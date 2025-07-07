using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SubjectId { get; set; }
        public string Text { get; set; }
        public string Difficulty { get; set; } // "Easy", "Normal", "Hard"
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Subject Subject { get; set; }
        public Admin CreatedBy { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
