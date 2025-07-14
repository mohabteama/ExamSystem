using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;


namespace ExamSystem.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }

        public bool CreateQuestion(Question question, int SubjectId)
        {
            var subject = _context.Subjects
                .Local
                .FirstOrDefault(s => s.Id == SubjectId);
            if (subject == null)
            {
                subject = _context.Subjects.Find(SubjectId);

                if (subject == null)
                {
                    throw new FileNotFoundException($"Subject with ID {SubjectId} not found.");
                }
            }

            question.Subject = subject;
            _context.Questions.Add(question);
            return _context.SaveChanges() > 0;
        }
    }  
}
