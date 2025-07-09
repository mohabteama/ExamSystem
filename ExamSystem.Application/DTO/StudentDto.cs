using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTO
{
    public class StudentDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
    }
}
