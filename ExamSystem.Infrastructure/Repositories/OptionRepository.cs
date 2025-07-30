using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Infrastructure.Repositories
{
    public class OptionRepository : GenericRepository<Option>, IOptionRepository
    {
        public OptionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Option>> GetOptions(int QuestionId)
        {
            return await _context.Options.Where(o => o.QuestionId == QuestionId).ToListAsync();
        }
    }
}
