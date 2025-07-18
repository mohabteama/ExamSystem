using ExamSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.IService
{
    public interface IExamService
    {
        bool CreateExam(ExamDto examDto , string studentId, int subjectId);
        //List<ExamDto> GetExamHistoryByStudentId(string studentId);
        //List<ExamDto> GetStudentExamsByStudentId(string studentId);
        Task<ExamSubmissionResultDto> SubmitExamAsync(ExamSubmissionDto submission);
        Task<PaginatedResultDto<ExamDto>> GetAllExamHistoryPagedAsync(int pageNumber, int pageSize, string status = null);
        Task<PaginatedResultDto<ExamDto>> GetStudentExamHistoryPagedAsync(string studentId, int pageNumber, int pageSize, string status = null);
    }
}
