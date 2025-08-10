using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Interfaces;


namespace ExamSystem.Application.Services.Service
{
    public class ExamResultService : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IMapper _mapper;
        public ExamResultService(IExamResultRepository examResultRepository, IMapper mapper)
        {
            _examResultRepository = examResultRepository;
            _mapper = mapper;
        }

        public async Task<ExamResultDto> GetExamResultAsync(string studentId, int examId)
        {
            var examResult = await _examResultRepository.GetExamResultAsync(studentId, examId);
            if (examResult == null)
                return null;    
            var Result =  _mapper.Map<ExamResultDto>(examResult);
            return Result;
        }
    }
}
