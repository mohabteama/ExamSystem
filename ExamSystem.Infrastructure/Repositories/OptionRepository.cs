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
    }
}
