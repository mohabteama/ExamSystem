

namespace ExamSystem.Application.DTO
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionDto> Questions { get; set; }

    }
    public class CreateSubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
