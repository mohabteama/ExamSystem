using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using System;
using static ExamSystem.Domain.Entities.Question;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ExamSystem.Application.Services.Service
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly IExamResultRepository _examResultRepository;
        private readonly IExamQuestionRepository _examQuestionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public ExamService(
            IExamRepository examRepository,
            ISubjectRepository subjectRepository,
            IStudentRepository studentRepository,
            IQuestionRepository questionRepository,
            IOptionRepository optionRepository,
            IStudentAnswerRepository studentAnswerRepository,
            IExamResultRepository examResultRepository,
            IExamQuestionRepository examQuestionRepository,
            IMapper _mapper)
        {
            _subjectRepository = subjectRepository;
            _examRepository = examRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _studentAnswerRepository = studentAnswerRepository;
            _examResultRepository = examResultRepository;
            _examQuestionRepository = examQuestionRepository;
            this._mapper = _mapper;
        }
        public async Task<PaginatedResultDto<ExamResultDto>> GetAllExamHistoryPagedAsync(int pageNumber, int pageSize, string status = null)
        {
            var (exams, totalCount) = await _examRepository.GetAllExamsPagedAsync(pageNumber, pageSize, status);

            var examResultDtos = _mapper.Map<List<ExamResultDto>>(exams);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            
            
            return new PaginatedResultDto<ExamResultDto>
            {
                Items = examResultDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
        }


            public async Task<bool> CreateRondomQuestions(string studentId, int subjectId)
            {



                var questions = await _examRepository.CreateRondomExamQuestions(subjectId, 10);
                Exam exam = new Exam
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    StartTime = DateTime.UtcNow,
                    Status = "InProgress",
                    Questions = questions
                };
                var result = await _examRepository.AddAsync(exam);
                return result;
            }
        public async Task<PaginatedResultDto<ExamHistoryDto>> GetStudentExamHistoryPagedAsync(
            string studentId, int pageNumber, int pageSize, string status = null)
        {

            var student = _studentRepository.GetByStringId(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var (exams, totalCount) = await _examRepository.GetStudentExamHistoryPagedAsync(
                studentId, pageNumber, pageSize, status);

            var examDtos = _mapper.Map<List<ExamHistoryDto>>(exams);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResultDto<ExamHistoryDto>
            {
                Items = examDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public async Task<ExamDto> GetExamWithQuestions(int examId)
        {
            var exam = await _examRepository.GetExamDetails(examId);
            

            if (exam == null)
                return null;

            return new ExamDto
            {
                Id = exam.Id,
                StartTime = DateTime.Now,
                SubjectName = exam.Subject?.Name,
                Questions = exam.Questions?.Select(q => new Question
                {
                    Id = q.Id,
                    question = q.question,
                    Options = q.Options.Select(o => new Option
                    {
                        Id = o.Id,
                        option = o.option,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<CreateExamDto> CreateExam(string studentId, int subjectId)
        {
            Random random = new Random();
            int randomFrom1To20 = random.Next(7, 10);

            var rondomquestions = await _examRepository.CreateRondomExamQuestions(subjectId, randomFrom1To20);

            var exam = await _examRepository.AddExam(new Exam
            {
                StudentId = studentId,
                SubjectId = subjectId,
                StartTime = DateTime.UtcNow,
                Status = "InProgress",
                Questions = rondomquestions
            });

            if (exam == null)
            {
                throw new ArgumentException("Exam creation failed.");
            }

            var result = _mapper.Map<CreateExamDto>(exam);
            return result;
        }


        public async Task<SubmitDto> Submit(ExamSubmissionInputDto input)
        {
            var exam = await _examRepository.CalculateExamScoreAsync(input.ExamId);
            if (exam == null)
                return null;
            
            int score = 0;
            foreach (var question in exam.Subject.Questions)
            {
                var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                if (correctOption != null && input.SelectedOptionIds.Contains(correctOption.Id))
                {
                    score++;
                }
            exam.Score = score;
                 _examRepository.Save();
            }
            return new SubmitDto
            {
                ExamId = exam.Id,
                StudentId = exam.StudentId,
                Score = exam.Score,
                ExamDate = exam.StartTime,
                SubjectName = exam.Subject.Name
            };

        }
    }
} 
