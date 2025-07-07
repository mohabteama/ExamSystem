using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class StudentAnswer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
        public string SelectedOptionId { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
    }
}
