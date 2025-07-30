using ExamSystem.Domain.Entities;

namespace ExamSystem.Domain.Interfaces
{
    public interface IExamQuestionRepository : IGenericRepository<ExamQuestion>
    {
        Task<IEnumerable<ExamQuestion>> GetByExamIdAsync(int examId);
        //Task<ICollection<ExamQuestion>> CreateRondomExamQuestions(int subjectId);
        
    }}
