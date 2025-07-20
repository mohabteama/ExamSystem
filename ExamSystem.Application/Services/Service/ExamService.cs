using AutoMapper;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;


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
        private readonly IMapper _mapper;

        public ExamService(
            IExamRepository examRepository,
            IStudentRepository studentRepository,
            IQuestionRepository questionRepository,
            IOptionRepository optionRepository,
            IStudentAnswerRepository studentAnswerRepository,
            IExamResultRepository examResultRepository,
            IExamQuestionRepository examQuestionRepository,
            IMapper _mapper)
        {
            _examRepository = examRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _studentAnswerRepository = studentAnswerRepository;
            _examResultRepository = examResultRepository;
            _examQuestionRepository = examQuestionRepository;
            this._mapper = _mapper;
        }
        public async Task<PaginatedResultDto<ExamDto>> GetAllExamHistoryPagedAsync(int pageNumber, int pageSize, string status = null)
        {
            var (exams, totalCount) = await _examRepository.GetAllExamsPagedAsync(pageNumber, pageSize, status);

            var examDtos = _mapper.Map<List<ExamDto>>(exams);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResultDto<ExamDto>
            {
                Items = examDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public async Task<ExamSubmissionResultDto> SubmitExamAsync(ExamSubmissionDto submission)
        {
            var student =  _studentRepository.GetByStringId(submission.StudentId);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var exam =  _examRepository.GetByIntId(submission.ExamId);
            if (exam == null)
            {
                throw new ArgumentException("Exam not found");
            }

            var examQuestions = await _examQuestionRepository.GetByExamIdAsync(submission.ExamId);
            var examQuestionIds = examQuestions.Select(eq => eq.QuestionId).ToHashSet();

            foreach (var answer in submission.Answers)
            {
                if (!examQuestionIds.Contains(answer.QuestionId))
                {
                    throw new ArgumentException($"Question {answer.QuestionId} is not part of this exam");
                }
            }

            int correctAnswers = 0;
            int totalScore = 0;
            List<StudentAnswer> studentAnswers = new List<StudentAnswer>();

            foreach (var answer in submission.Answers)
            {
                var question =  _questionRepository.GetByIntId(answer.QuestionId);
                if (question == null)
                {
                    throw new ArgumentException($"Question {answer.QuestionId} not found");
                }

                var selectedOption =  _optionRepository.GetByIntId(answer.OptionId);
                if (selectedOption == null || selectedOption.QuestionId != question.Id)
                {
                    throw new ArgumentException($"Option {answer.OptionId} not found for question {answer.QuestionId}");
                }

                var studentAnswer = new StudentAnswer
                {
                    ExamId = submission.ExamId,
                    StudentId = submission.StudentId,
                    QuestionId = answer.QuestionId,
                    SelectedOptionId = answer.OptionId,
                };

                studentAnswers.Add(studentAnswer);

                if (selectedOption.IsCorrect)
                {
                    correctAnswers++;
                    totalScore += question.Score;
                }
            }

            foreach (var answer in studentAnswers)
            {
                await _studentAnswerRepository.AddAsync(answer);
            }

            var examResult = new ExamResult
            {
                ExamId = submission.ExamId,
                StudentId = submission.StudentId,
                Score = totalScore,
            };

            await _examResultRepository.AddAsync(examResult);

            return new ExamSubmissionResultDto
            {
                ResultId = examResult.Id,
                Score = totalScore,
                TotalQuestions = examQuestions.Count(),
                CorrectAnswers = correctAnswers,
                
            };
        }
        public bool CreateExam(ExamDto examDto, string studentId, int subjectId)
        {
            var exam = _mapper.Map<Exam>(examDto);
            return _examRepository.Create(exam);
        }
        public async Task<PaginatedResultDto<ExamDto>> GetStudentExamHistoryPagedAsync(
            string studentId, int pageNumber, int pageSize, string status = null)
        {

            var student = _studentRepository.GetByStringId(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var (exams, totalCount) = await _examRepository.GetStudentExamHistoryPagedAsync(
                studentId, pageNumber, pageSize, status);

            var examDtos = _mapper.Map<List<ExamDto>>(exams);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResultDto<ExamDto>
            {
                Items = examDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
