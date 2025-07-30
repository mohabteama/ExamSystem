
namespace ExamSystem.Application.DTO
{
    public class StudentAnswerDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public string SelectedOptionId { get; set; }
    }
}
