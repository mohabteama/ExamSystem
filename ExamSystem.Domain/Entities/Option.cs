
namespace ExamSystem.Domain.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
