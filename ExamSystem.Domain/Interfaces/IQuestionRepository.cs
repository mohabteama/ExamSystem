using ExamSystem.Domain.Entities;


namespace ExamSystem.Domain.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        public Question CreateQuestion(Question question, int SubjectId);
    }
}
