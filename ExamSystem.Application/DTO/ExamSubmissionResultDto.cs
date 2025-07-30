
namespace ExamSystem.Application.DTO
{
    public class ExamSubmissionResultDto
    {
        public int ResultId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
