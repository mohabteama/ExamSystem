using ExamSystem.Application.DTO;

namespace ExamSystem.Application.Services.IService
{
    public interface IStudentService
    {
        List<StudentDto> GetAllStudents();
        //bool CreateStudent(StudentDto StudentDto);
        bool UpdateStudentStatus(string studentId,bool isActive);
        Task<PaginatedResultDto<StudentDto>> GetStudentsPaginatedAsync(int pageNumber, int pageSize, bool? isActive = null);
    }
}
