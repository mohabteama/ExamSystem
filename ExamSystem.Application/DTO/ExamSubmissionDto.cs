using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamSubmissionDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }

        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    }
}
