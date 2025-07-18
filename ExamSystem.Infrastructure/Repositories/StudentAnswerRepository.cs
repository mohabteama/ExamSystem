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
    public class StudentAnswerRepository : GenericRepository<StudentAnswer>, IStudentAnswerRepository
    {
        public StudentAnswerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<StudentAnswer>> GetByExamAndStudentIdAsync(int examId, string studentId)
        {
            return await _context.Set<StudentAnswer>()
                .Where(sa => sa.ExamId == examId && sa.StudentId == studentId)
                .ToListAsync();
        }
    }
}
