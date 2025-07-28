using ExamSystem.Domain.Entities;


namespace ExamSystem.Domain.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        public Task<Subject> GetSubjectWithQuestions(int id);
    }
}
