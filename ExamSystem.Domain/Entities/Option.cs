
namespace ExamSystem.Domain.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string option { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
