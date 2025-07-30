using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamResultRepository : GenericRepository<ExamResult> , IExamResultRepository
    {
        public ExamResultRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Exam> GetExamResultAsync(string studentId, int examId)
        {
            var examResult = await _context.Exams
                .Include(r => r.Result)
                .Include(r => r.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.Id == examId);
            return examResult;
        }
    }
}
