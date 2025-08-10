using ExamSystem.Application.DTO;
using ExamSystem.Domain.Entities;


namespace ExamSystem.Application.Services.IService
{
    public interface IQuestionService
    {
       public Question CreateQuestion(CreateQuestionDto CreateQuestionDto, int SubjectId);
    }
}
