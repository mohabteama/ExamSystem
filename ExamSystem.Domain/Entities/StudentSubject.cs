using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class StudentSubject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public string SubjectId { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
