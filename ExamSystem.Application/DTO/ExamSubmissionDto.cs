
namespace ExamSystem.Application.DTO
{
    public class SubmissionDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public List<string>Answer { get; set; } = new List<string>();
        public int Score { get; set; } = 0;

    }
}
