using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class UserRegistrationRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
}
