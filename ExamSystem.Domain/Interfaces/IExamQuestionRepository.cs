using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Interfaces
{
    public interface IExamQuestionRepository : IGenericRepository<ExamQuestion>
    {
        Task<IEnumerable<ExamQuestion>> GetByExamIdAsync(int examId);
    }}
