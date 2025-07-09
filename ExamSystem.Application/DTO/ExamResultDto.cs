using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamResultDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ExamId { get; set; }
        public string StudentId { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }
    }
}
