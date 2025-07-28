using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Interfaces
{
    public interface IOptionRepository : IGenericRepository<Option>
    {
        public Task<List<Option>> GetOptions(int QuestionId);
    }
}
