using ExamSystem.Domain.Entities;

namespace ExamSystem.Domain.Interfaces
{
    public interface IStudentAnswerRepository : IGenericRepository<StudentAnswer>
    {
        Task<IEnumerable<StudentAnswer>> GetByExamAndStudentIdAsync(int examId, string studentId);
    }
}
