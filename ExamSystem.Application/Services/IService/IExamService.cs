using ExamSystem.Application.DTO;


namespace ExamSystem.Application.Services.IService
{
    public interface IExamService
    {
        public Task<ExamDto> GetExamWithQuestions(int examId);
        Task<bool> CreateExam(string studentId, int subjectId );
        //List<ExamDto> GetExamHistoryByStudentId(string studentId);
        //List<ExamDto> GetStudentExamsByStudentId(string studentId);
        Task<ExamSubmissionResultDto> SubmitExamAsync(ExamSubmissionDto submission);
        Task<PaginatedResultDto<ExamResultDto>> GetAllExamHistoryPagedAsync(int pageNumber, int pageSize, string status = null);
        Task<PaginatedResultDto<ExamDto>> GetStudentExamHistoryPagedAsync(string studentId, int pageNumber, int pageSize, string status = null);
    }
}
