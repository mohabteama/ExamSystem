
namespace ExamSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        T GetByIntId(int id);
        T GetByStringId(string id);
        bool Create(T entity);
        bool Update(T entity);
        bool Save();
    }
}
