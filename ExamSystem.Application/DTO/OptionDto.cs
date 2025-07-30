
namespace ExamSystem.Application.DTO
{
    public class OptionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string option { get; set; }
        public bool IsCorrect { get; set; }
    }
}
