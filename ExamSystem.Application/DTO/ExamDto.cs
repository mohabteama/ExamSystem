using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class ExamDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public string SubjectId { get; set; }
        public string Difficulty { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = "InProgress";
    }
}
