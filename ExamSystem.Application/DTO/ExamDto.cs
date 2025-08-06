using ExamSystem.Domain.Entities;

namespace ExamSystem.Application.DTO
{
    public class ExamDto
    {
        public int Id { get; set; }
        public int Score { get; set; } = 0;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public ICollection<Question> Questions { get; set; }
        public string SubjectName { get; set; }
    }
    public class ExamHistoryDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int Score { get; set; } = 0;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        
        public string SubjectName { get; set; }
    }
}
