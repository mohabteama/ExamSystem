
namespace ExamSystem.Application.DTO
{
    public class ExamResultDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public string SubjectName { get; set; }
        public int Score { get; set; } = 10;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
    }
}
