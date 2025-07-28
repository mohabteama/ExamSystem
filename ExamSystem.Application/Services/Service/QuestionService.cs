using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;


namespace ExamSystem.Application.Services.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository; 
        private readonly IMapper _mapper;
        public QuestionService(IMapper mapper , IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        
        public bool CreateQuestion(CreateQuestionDto CreateQuestionDto, int SubjectId)
        {
            var exist = _questionRepository.GetAll()
                .Any(q => q.question.Trim().ToLower() ==
                CreateQuestionDto.question.Trim().ToLower());
            if (exist)
                return false;
            var question = _mapper.Map<Question>(CreateQuestionDto);
            return _questionRepository.CreateQuestion(question, SubjectId);
        }   
    }
}
