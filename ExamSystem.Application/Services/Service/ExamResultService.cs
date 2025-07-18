using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<ExamResultDto> GetExamResultAsync(string studentId, int examId)
        {
            var examResult = _examResultRepository.GetExamResult(studentId, examId);
            if (examResult == null)
                return Task.FromResult<ExamResultDto>(null);
            return Task.FromResult(_mapper.Map<ExamResultDto>(examResult));
        }
    }
}
