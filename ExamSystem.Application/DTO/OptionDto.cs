using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class OptionDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
