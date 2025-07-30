using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;

namespace ExamSystem.Infrastructure.Repositories
{
    public class StudentSubjectRepository : GenericRepository<StudentSubject>, IStudentSubjectRepository
    {
        public StudentSubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
