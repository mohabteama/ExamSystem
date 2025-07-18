using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;
        public DashBoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<int> GetDashBoard()
        {
            var Total = new List<int>
            {
                _context.Students.CountAsync().Result,
                _context.Subjects.CountAsync().Result,
                _context.Exams.CountAsync().Result,
            };
            return Total;
        }
    }
}