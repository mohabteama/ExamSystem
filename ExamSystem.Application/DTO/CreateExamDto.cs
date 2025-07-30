
namespace ExamSystem.Application.DTO
{
    public class CreateExamDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
    }
}
