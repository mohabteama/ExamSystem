using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamQuestionRepository : GenericRepository<ExamQuestion>, IExamQuestionRepository
    {
        public ExamQuestionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ExamQuestion>> GetByExamIdAsync(int examId)
        {
            return await _context.Set<ExamQuestion>()
                .Where(eq => eq.ExamId == examId)
                .ToListAsync();
        }
    }
}
