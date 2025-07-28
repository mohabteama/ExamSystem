using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;


namespace ExamSystem.Application.Services.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public SubjectService(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public List<SubjectDto> GetAllSubjects()
        {
            return _mapper.Map<List<SubjectDto>>(_subjectRepository.GetAll());
        }

        public bool CreateSubject(CreateSubjectDto CreateSubjectDto)
        {
            var exist = _subjectRepository.GetAll()
                .Any(s => s.Name.Trim().ToLower() == CreateSubjectDto.Name.Trim().ToLower());
            if (exist)
                return false;
            var subject = _mapper.Map<Subject>(CreateSubjectDto);
            return _subjectRepository.Create(subject);
        }
        public bool UpdateSubject(SubjectDto SubjectDto, int SubjectId)
        {
            var exist = _subjectRepository.GetByIntId(SubjectId);
            if (exist == null)
                return false;
            var subject = _mapper.Map<Subject>(SubjectDto);
            return _subjectRepository.Update(subject);
        }
        public async Task<SubjectDto> GetSubjectWithQuestions(int SubjectId) {
            var subject = await _subjectRepository.GetSubjectWithQuestions(SubjectId);
            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Questions = subject.Questions?.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Text = q.question,
                    Options = q.Options?.Select(o => new OptionDto
                    {
                        Id = o.Id,
                        QuestionId = o.QuestionId,
                        Answer = o.Answer,
                        IsCorrect = o.IsCorrect
                    }).ToList() ?? new List<OptionDto>()
                }).ToList() ?? new List<QuestionDto>(),
            };
        }
    }
}
