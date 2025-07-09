
namespace ExamSystem.Domain.Entities
{
    public class Question
    {
        public enum DifficultyLevel
        {
            Easy = 1,
            Medium = 2,
            Hard = 3
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SubjectId { get; set; }
        public string question { get; set; }
        public int TotalQuestionsCount { get; set; }
        public string AdminId { get; set; }
        public bool IsActive { get; set; } = true;
        public DifficultyLevel Difficulty { get; set; }

        public Subject Subject { get; set; }
        public Admin CreatedByAdmin { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
