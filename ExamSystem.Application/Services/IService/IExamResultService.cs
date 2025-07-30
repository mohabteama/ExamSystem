using ExamSystem.Application.DTO;

namespace ExamSystem.Application.Services.IService
{
    public interface IExamResultService 
    {
        Task<ExamResultDto> GetExamResultAsync(string studentId, int examId);
    }
}
