using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context) : base(context) { }
        public List<Exam> GetExamsByStudentId(string studentId)
        {
            return _context.Exams
                .Where(e => e.StudentId == studentId)
                .ToList();
        }

        public List<Exam> GetStudentExamsByStudentId(string studentId)
        {
            return _context.Exams
                .Where(er => er.StudentId == studentId)
                .ToList();
        }
        public  Task<Exam?> GetExamWithQuestionsAndOptionsAsync(int id)
        {
            return  _context.Exams
                .Include(e => e.ExamQuestions)
                    .Include(q => q.StudentAnswers)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<(List<Exam> Exams, int TotalCount)> GetAllExamsPagedAsync(int pageNumber, int pageSize, string status = null)
        {
            // Ensure valid pagination parameters
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Build query
            IQueryable<Exam> query = _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Include(e => e.Result);

            // Apply status filter if provided
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            // Get total count
            int totalCount = await query.CountAsync();

            // Apply pagination
            var exams = await query
                .OrderByDescending(e => e.StartTime) // Most recent exams first
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (exams, totalCount);
        }
        public async Task<(List<Exam> Exams, int TotalCount)> GetStudentExamHistoryPagedAsync(
            string studentId, int pageNumber, int pageSize, string status = null)
        {
            // Ensure valid pagination parameters
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Build query with student filter
            IQueryable<Exam> query = _context.Exams
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Subject)
                .Include(e => e.Result);

            // Apply status filter if provided
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            // Get total count
            int totalCount = await query.CountAsync();

            // Apply pagination
            var exams = await query
                .OrderByDescending(e => e.StartTime) // Most recent exams first
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (exams, totalCount);
        }
    }
}
