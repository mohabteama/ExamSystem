using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context){ }
        public async Task<Subject> GetSubjectWithQuestions(int id)
        {
        var result = await _context.Subjects
            .Include(q => q.Questions)
            .ThenInclude(o=>o.Options)
            .FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }
    }
}
