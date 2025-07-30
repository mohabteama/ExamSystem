using ExamSystem.Domain.Entities;

namespace ExamSystem.Domain.Interfaces
{
    public interface IExamResultRepository : IGenericRepository<ExamResult>
    {
        public Task<Exam> GetExamResultAsync(string studentId, int examId);
    }
}
