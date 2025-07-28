
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

        public int Id { get; set; } 
        public int Score { get; set; } 
        public int SubjectId { get; set; }

        public string question { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public Subject Subject { get; set; }

        
        public ICollection<Option> Options { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
   
    }
}
