
using static ExamSystem.Domain.Entities.Question;

namespace ExamSystem.Application.DTO
{
    public class QuestionDto
    {
        public string question { get; set; }
        public DifficultyLevel Difficulty { get; set; }
    }
}
