using ExamSystem.Application.DTO;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.IService
{
    public interface ISubjectService
    {
        List<SubjectDto> GetAllSubjects();
        bool CreateSubject(SubjectDto SubjectDto);
        bool UpdateSubject(SubjectDto SubjectDto, int SubjectId);
    }
}
