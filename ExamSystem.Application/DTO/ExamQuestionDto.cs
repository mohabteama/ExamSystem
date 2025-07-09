using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamQuestionDto
    {
        public string Id { get; set; }
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
    }
}
