

namespace ExamSystem.Domain.Entities
{
    public class Exam
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public string SubjectId { get; set; }
        public string Difficulty { get; set; } // "Easy", "Normal", "Hard"
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = "InProgress"; // "InProgress", "Submitted", "TimedOut", "Evaluated"

        // Navigation properties
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public ExamResult Result { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
