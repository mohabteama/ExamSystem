
namespace ExamSystem.Application.DTO
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string? Username { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
