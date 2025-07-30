using ExamSystem.Application.DTO;

namespace ExamSystem.Application.Services.IService
{
    public interface IStudentSubjectService
    {
        List<StudentSubjectDto> GetAllStudentSubject();
        bool CreateSubject(StudentSubjectDto StudentSubjectDto);
    }
}
