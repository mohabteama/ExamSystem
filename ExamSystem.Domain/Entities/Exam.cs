using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public string SubjectId { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = "InProgress"; // "InProgress", "Submitted", "TimedOut", "Evaluated"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public ExamResult Result { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
