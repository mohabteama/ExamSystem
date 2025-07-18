using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Interfaces
{
    public interface IDashBoardRepository
    {
        public List<int> GetDashBoard();
    }
}
