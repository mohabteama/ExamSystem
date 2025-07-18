namespace ExamSystem.Domain.Entities
{
    public class Exam
    {
        
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Difficulty { get; set; }
        public int TotalQuestionsCount { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = "InProgress";

        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public ExamResult Result { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
