using ExamSystem.Application.DTO;

namespace ExamSystem.Application.Services.IService
{
    public interface IOptionService
    {
        bool CreateOptions(OptionDto optionDto, int questionId);
    }
}
