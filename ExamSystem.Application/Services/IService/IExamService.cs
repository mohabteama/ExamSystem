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
        bool CreateExam(ExamDto examDto , int studentId, int subjectId);
        List<ExamDto> GetExamHistoryByStudentId(int studentId);
    }
}
