using ExamSystem.Application.DTO;


namespace ExamSystem.Application.Services.IService
{
    public interface ISubjectService
    {
        public Task<SubjectDto> GetSubjectWithQuestions(int SubjectId);
        List<SubjectDto> GetAllSubjects();
        bool CreateSubject(CreateSubjectDto CreateSubjectDto);
        bool UpdateSubject(SubjectDto SubjectDto, int SubjectId);
    }
}
