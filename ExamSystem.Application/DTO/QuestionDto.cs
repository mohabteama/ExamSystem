
namespace ExamSystem.Application.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string question { get; set; }
        public List<OptionDto> Options { get; set; } = new();
    }




    public class CreateQuestionDto
    {
        public string question { get; set; }

    }
}
