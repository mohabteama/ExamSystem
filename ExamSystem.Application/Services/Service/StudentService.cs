using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
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
            return _mapper.Map<List<StudentDto>>(_StudentRepository.GetAll());
        }
        public bool CreateStudent(StudentDto StudentDto)
        {
            var exist = _StudentRepository.GetAll()
                .Any(s => s.Email.Trim().ToLower() == StudentDto.Email.Trim().ToLower());
            if (exist)
                return false;
            var student = _mapper.Map<Student>(StudentDto);
            return _StudentRepository.Create(student);
        }

        public bool UpdateStudentStatus(int StudentId,bool isActive)
        {
            var student = _StudentRepository.GetById(StudentId);
            student.IsActive = isActive;
            return _StudentRepository.Save();
        }
    }
}
