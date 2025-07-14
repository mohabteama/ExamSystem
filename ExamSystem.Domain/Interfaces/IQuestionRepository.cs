using ExamSystem.Domain.Entities;


namespace ExamSystem.Domain.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        bool CreateQuestion(Question question, int SubjectId);
    }
}
