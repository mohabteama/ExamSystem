using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Interfaces
{
    public interface IExamResultRepository : IGenericRepository<ExamResult>
    {
        public ExamResult GetExamResult(string studentId, int examId);
        Task<ExamResult?> GetByExamAndStudentIdAsync(int examId, string studentId);
    }
}
