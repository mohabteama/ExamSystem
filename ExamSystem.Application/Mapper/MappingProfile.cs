using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Domain.Entities;

namespace ExamSystem.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamQuestion, ExamQuestionDto>().ReverseMap();
            CreateMap<ExamResult, ExamResultDto>().ReverseMap();
            CreateMap<Option, OptionDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<StudentAnswer, StudentAnswerDto>().ReverseMap();
            CreateMap<StudentSubject, StudentSubjectDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}
