
namespace ExamSystem.Domain.Entities
{
    public class Subject
    {
            public int Id { get; set; }
            public string Name { get; set; }
            
        public ICollection<Question> Questions { get; set; }
        public ICollection<Exam> Exams { get; set; }
        //public ICollection<Student> Students { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
