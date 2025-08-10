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

        public Question CreateQuestion(CreateQuestionDto createQuestionDto, int subjectId)
        {
            var exist = _questionRepository.GetAll()
                .Any(q => q.question.Trim().ToLower() == createQuestionDto.question.Trim().ToLower());

            if (exist)
                return null; // علشان تعرف في الـ Controller إنه موجود بالفعل

            var question = _mapper.Map<Question>(createQuestionDto);
            return _questionRepository.CreateQuestion(question, subjectId);
        }


    }
}
