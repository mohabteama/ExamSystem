using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;

namespace ExamSystem.Application.Services.Service
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IMapper _mapper;

        public StudentSubjectService(IMapper mapper, IStudentSubjectRepository studentSubjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
        }
        public bool CreateSubject(StudentSubjectDto studentSubjectDto)
        {
            var exist = _studentSubjectRepository.GetAll()
                .Any(s => s.StudentId == studentSubjectDto.StudentId && s.SubjectId == studentSubjectDto.SubjectId);
            if (exist)
                return false;
            var studentSubject = _mapper.Map<StudentSubject>(studentSubjectDto);
            return _studentSubjectRepository.Create(studentSubject);
        }

        public List<StudentSubjectDto> GetAllStudentSubject()
        {
            return _mapper.Map<List<StudentSubjectDto>>(_studentSubjectRepository.GetAll());
        }
    }
}
