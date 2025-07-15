
namespace ExamSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        IQueryable<T> Get();
        T GetByName(string name);
        bool Exists(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
