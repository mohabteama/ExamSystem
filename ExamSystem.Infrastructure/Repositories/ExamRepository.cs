using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context) : base(context) { }
        public List<Exam> GetExamsByStudentId(int studentId)
        {
            return _context.Exams
                .Where(e => e.StudentId == studentId)
                .ToList();
        }
    }
}
