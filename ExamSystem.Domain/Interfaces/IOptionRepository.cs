using ExamSystem.Domain.Entities;

namespace ExamSystem.Domain.Interfaces
{
    public interface IOptionRepository : IGenericRepository<Option>
    {
        public Task<List<Option>> GetOptions(int QuestionId);
    }
}
