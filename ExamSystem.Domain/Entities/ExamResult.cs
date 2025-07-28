using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public string SubjectName { get; set; }
        public int Score { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public bool IsPassed { get; set; }

        public Exam Exam { get; set; }
        public Student Student { get; set; }
    }
}
