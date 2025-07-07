
namespace ExamSystem.Domain.Entities
{
    public class Admin
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        // Navigation properties
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
