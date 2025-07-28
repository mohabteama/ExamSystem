using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context) : base(context) { }



        public Task<Exam> GetExamDetails(int id)
        {
            return  _context.Exams
                .Include(s => s.Subject)
                    .ThenInclude(q => q.Questions)
                        .ThenInclude(o => o.Options)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


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

        public async Task<(List<Exam> Exams, int TotalCount)> GetAllExamsPagedAsync(int pageNumber, int pageSize, string status = null)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            IQueryable<Exam> query = _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Include(e => e.Result);

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            int totalCount = await query.CountAsync();

            var exams = await query
                .OrderByDescending(e => e.StartTime) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (exams, totalCount);
        }
        public async Task<(List<Exam> Exams, int TotalCount)> GetStudentExamHistoryPagedAsync(
            string studentId, int pageNumber, int pageSize, string status = null)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            IQueryable<Exam> query = _context.Exams
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Subject)
                .Include(e => e.Result);

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            int totalCount = await query.CountAsync();

            var exams = await query
                .OrderByDescending(e => e.StartTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (exams, totalCount);
        }












        public async Task<ICollection<Question>> CreateRondomExamQuestions(int subjectId, int numberOfQuestions)
        {
            var randomQuestions = await _context.Questions
                .Include(q => q.Options)
                .Where(q => q.SubjectId == subjectId)
                .OrderBy(q => Guid.NewGuid()) // Randomly order the questions
                .Take(numberOfQuestions) // Take 10 random questions
                .ToListAsync();
            return (ICollection<Question>)randomQuestions;
        }
    }
}
