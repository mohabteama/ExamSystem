
namespace ExamSystem.Domain.Entities
{
    public class StudentSubject
    {
        public string StudentId { get; set; }
        public int SubjectId { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
