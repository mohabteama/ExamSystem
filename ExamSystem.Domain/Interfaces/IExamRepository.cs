using ExamSystem.Domain.Entities;


namespace ExamSystem.Domain.Interfaces
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        public Task<Exam> GetExamDetails(int id);
        Task<ICollection<Question>> CreateRondomExamQuestions(int subjectId, int numberOfQuestions);
        public List<Exam> GetExamsByStudentId(string studentId);
        public List<Exam> GetStudentExamsByStudentId(string studentId);
        Task<(List<Exam> Exams, int TotalCount)> GetAllExamsPagedAsync(int pageNumber, int pageSize, string status = null);
        Task<(List<Exam> Exams, int TotalCount)> GetStudentExamHistoryPagedAsync(string studentId, int pageNumber, int pageSize, string status = null);
    }
}
