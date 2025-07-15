using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;


namespace ExamSystem.Application.Services.Service
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;
        public OptionService(IMapper mapper, IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        public bool CreateOptions(OptionDto optionDto, int questionId) {
            
            var options = _mapper.Map<Option>(optionDto);
            return _optionRepository.Create(options);
        }
    }
}
