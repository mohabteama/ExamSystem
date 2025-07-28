
namespace ExamSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        Task<(ICollection<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<bool> AddAsync(T entity);
        T GetByIntId(int id);
        T GetByStringId(string id);
        IQueryable<T> Get();
        T GetByName(string name);
        bool Exists(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
