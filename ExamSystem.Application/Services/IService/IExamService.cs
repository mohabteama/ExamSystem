using ExamSystem.Application.DTO;


namespace ExamSystem.Application.Services.IService
{
    public interface IExamService
    {
        public Task<ExamDto> GetExamWithQuestions(int examId);
        Task<PaginatedResultDto<ExamResultDto>> GetAllExamHistoryPagedAsync
            (int pageNumber, int pageSize, string status = null);
        Task<PaginatedResultDto<ExamHistoryDto>> GetStudentExamHistoryPagedAsync
            (string studentId, int pageNumber, int pageSize, string status = null);
        Task<CreateExamDto> CreateExam(string studentId , int subjectId);
        Task<SubmitDto> Submit(ExamSubmissionInputDto input);
    }
}
