using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Option
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }

        // Navigation properties
        public Question Question { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
