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
    public class DashBoardService : IDashBoardService
    {
        private readonly IDashBoardRepository _dashBoardRepository;
       
        public DashBoardService(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
            
        }
        public DashBoardDto GetDashBoardData()
        {
            return new DashBoardDto
            {
                TotalStudents = _dashBoardRepository.GetDashBoard()[0],
                TotalSubjects = _dashBoardRepository.GetDashBoard()[1],
                TotalExams = _dashBoardRepository.GetDashBoard()[2],
            };
        }
    }
}
