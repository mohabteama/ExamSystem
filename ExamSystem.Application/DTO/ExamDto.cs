using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public ICollection<Question> Questions { get; set; }
        public string SubjectName { get; set; }
    }
}
