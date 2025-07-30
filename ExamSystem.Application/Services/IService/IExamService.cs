using ExamSystem.Application.DTO;


namespace ExamSystem.Application.Services.IService
{
    public interface IExamService
    {
        public Task<ExamDto> GetExamWithQuestions(int examId);
        Task<bool> CreateRondomQuestions(string studentId, int subjectId );
        Task<PaginatedResultDto<ExamResultDto>> GetAllExamHistoryPagedAsync
            (int pageNumber, int pageSize, string status = null);
        Task<PaginatedResultDto<ExamDto>> GetStudentExamHistoryPagedAsync
            (string studentId, int pageNumber, int pageSize, string status = null);
        //Task<SubmissionDto> submit(SubmissionDto submitDto , string studentId, int examId);
        Task<CreateExamDto> CreateExam(string studentId , int subjectId);
    }
}
