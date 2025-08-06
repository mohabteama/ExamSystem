using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static ExamSystem.Domain.Entities.Question;


namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Exam> AddExam(Exam entity)
        {
            await _context.Set<Exam>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<Exam> GetExamDetails(int id)
        {
            return _context.Exams
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

            var subject = await _context.Subjects
                .Include(q => q.Questions)
                    .ThenInclude(o => o.Options)
                .FirstOrDefaultAsync(s => s.Id == subjectId);


            var randomQuestions = subject.Questions
                .GroupBy(q => q.Id)
                .Select(g => g.First())
                .OrderBy(q => Guid.NewGuid())
                .Take(numberOfQuestions)
                .ToList();


            return randomQuestions;
        }






        public async Task<Exam> CalculateExamScoreAsync(int examId)
        {
            var exam = await _context.Exams
                .Include(e => e.Subject)
                    .ThenInclude(eq => eq.Questions)
                        .ThenInclude(q => q.Options)
                .Include(e => e.StudentAnswers)
                    .ThenInclude(sa => sa.SelectedOption)
                .FirstOrDefaultAsync(e => e.Id == examId);

            return exam;    

        }
    }
}
