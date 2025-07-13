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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;    
        private readonly IMapper _mapper;

        public SubjectService(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public bool CreateSubject(SubjectDto SubjectDto)
        {
            var exist = _subjectRepository.GetAll()
                .Any(s => s.Name.Trim().ToLower() == SubjectDto.Name.Trim().ToLower());
            if (exist)
                return false;
            var subject = _mapper.Map<Subject>(SubjectDto);
            return _subjectRepository.Create(subject);

        }
    }
}
