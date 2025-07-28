using ExamSystem.Application.DTO;


namespace ExamSystem.Application.Services.IService
{
    public interface IQuestionService
    {
        bool CreateQuestion(CreateQuestionDto CreateQuestionDto, int SubjectId);
    }
}
