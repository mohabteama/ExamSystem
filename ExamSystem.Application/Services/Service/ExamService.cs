using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Service
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }
        public bool CreateExam(ExamDto examDto, int studentId, int subjectId)
        {
            var exam = _mapper.Map<Exam>(examDto);
            return _examRepository.Create(exam);
        }
        public List<ExamDto> GetExamHistoryByStudentId(int studentId)
        {
            var exams = _examRepository.GetExamsByStudentId(studentId);
            if (exams == null || !exams.Any())
                return new List<ExamDto>();
            return _mapper.Map<List<ExamDto>>(exams);
        }
    }
}
