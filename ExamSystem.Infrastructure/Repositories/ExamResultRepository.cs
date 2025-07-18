using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamResultRepository : GenericRepository<ExamResult> , IExamResultRepository
    {
        public ExamResultRepository(ApplicationDbContext context) : base(context) { }

        public ExamResult GetExamResult(string studentId, int examId)
        {
            var examResult = _context.ExamResults
                .FirstOrDefault(er => er.StudentId == studentId && er.ExamId == examId);
            return examResult;
        }
        public async Task<ExamResult?> GetByExamAndStudentIdAsync(int examId, string studentId)
        {
            return await _context.Set<ExamResult>()
                .FirstOrDefaultAsync(er => er.ExamId == examId && er.StudentId == studentId);
        }
    }
}
