using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }

        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
        public Student Student { get; set; }
    }
}
