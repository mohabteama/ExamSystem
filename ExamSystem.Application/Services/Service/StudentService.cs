using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Interfaces;

namespace ExamSystem.Application.Services.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepository;
        private readonly IMapper _mapper;

        public StudentService(IMapper mapper, IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
            _mapper = mapper;
        }
        public List<StudentDto> GetAllStudents() {
            var repoResult = _StudentRepository.GetAll();
            var students = _mapper.Map<List<StudentDto>>(repoResult);
            if (students == null)
                 throw new ArgumentException("No students found.");
            return students;
        }
        //public bool CreateStudent(StudentDto StudentDto)
        //{
        //    var exist = _StudentRepository.GetAll()
        //        .Any(s => s.Email.Trim().ToLower() == StudentDto.Email.Trim().ToLower());
        //    if (exist)
        //        return false;
        //    var student = _mapper.Map<Student>(StudentDto);
        //    return _StudentRepository.Create(student);
        //}

        public bool UpdateStudentStatus(string StudentId,bool isActive)
        {
            var student = _StudentRepository.GetByStringId(StudentId);
            student.IsActive = isActive;
            return _StudentRepository.Save();
        }
        public async Task<PaginatedResultDto<StudentDto>> GetStudentsPaginatedAsync(int pageNumber, int pageSize, bool? isActive = null)
        {
            var (students, totalCount) = await _StudentRepository.GetStudentsPagedAsync(pageNumber, pageSize, isActive);

            if (students == null || !students.Any())
                throw new ArgumentException("No students found.");

            // Map to DTOs
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResultDto<StudentDto>
            {
                Items = studentDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < totalPages
            };
        }
    }
}
