using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class ExamQuestion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
        public int Order { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
