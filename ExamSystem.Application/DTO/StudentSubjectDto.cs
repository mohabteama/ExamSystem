using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class StudentSubjectDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public string SubjectId { get; set; }
    }
}
