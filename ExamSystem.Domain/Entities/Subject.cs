using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class Subject
    {
            public string Id { get; set; } = Guid.NewGuid().ToString();
            public string Name { get; set; }
            public string AdminId { get; set; }

        public Admin CreatedByAdmin { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
