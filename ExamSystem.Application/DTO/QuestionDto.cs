using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class QuestionDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SubjectId { get; set; }
        public string question { get; set; }
        public int TotalQuestionsCount { get; set; }
        public string AdminId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
