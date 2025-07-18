using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamResultDto
    {
        public int Score { get; set; } = 10;
        public bool IsPassed { get; set; } = true;
    }
}
