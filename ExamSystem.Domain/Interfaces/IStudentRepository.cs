using ExamSystem.Domain.Entities;

namespace ExamSystem.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<(ICollection<Student> Students, int TotalCount)> GetStudentsPagedAsync(int pageNumber, int pageSize, bool? isActive = null);
    }
}
