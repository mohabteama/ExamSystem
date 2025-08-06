
namespace ExamSystem.Application.DTO
{
    public class SubmitDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public string SubjectName { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }
}
