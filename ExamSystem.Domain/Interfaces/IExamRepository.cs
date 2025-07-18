using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Interfaces
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        public List<Exam> GetExamsByStudentId(string studentId);
        public List<Exam> GetStudentExamsByStudentId(string studentId);
        Task<(List<Exam> Exams, int TotalCount)> GetAllExamsPagedAsync(int pageNumber, int pageSize, string status = null);
        Task<(List<Exam> Exams, int TotalCount)> GetStudentExamHistoryPagedAsync(string studentId, int pageNumber, int pageSize, string status = null);
    }
}
