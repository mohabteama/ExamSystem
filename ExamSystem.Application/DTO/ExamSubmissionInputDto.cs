using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamSubmissionInputDto
    {
        public int ExamId { get; set; }
        
        public List<int> SelectedOptionIds { get; set; }
    }
}
