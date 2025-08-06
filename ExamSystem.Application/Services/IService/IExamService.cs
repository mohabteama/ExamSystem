using ExamSystem.Application.DTO;
using static ExamSystem.Domain.Entities.Question;


namespace ExamSystem.Application.Services.IService
{
    public interface IExamService
    {
        public Task<ExamDto> GetExamWithQuestions(int examId);
        Task<bool> CreateRondomQuestions(string studentId, int subjectId);
        Task<PaginatedResultDto<ExamResultDto>> GetAllExamHistoryPagedAsync
            (int pageNumber, int pageSize, string status = null);
        Task<PaginatedResultDto<ExamHistoryDto>> GetStudentExamHistoryPagedAsync
            (string studentId, int pageNumber, int pageSize, string status = null);
        //Task<SubmissionDto> submit(SubmissionDto submitDto , string studentId, int examId);
        Task<CreateExamDto> CreateExam(string studentId , int subjectId);
        Task<SubmitDto> Submit(ExamSubmissionInputDto input);
    }
}
