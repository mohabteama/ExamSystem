using ExamSystem.Domain.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        
        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }


        public T GetByStringId(string id)
        {
            return _dbSet.Find(id);
        }
        public T GetByIntId(int id)
        {
            return _dbSet.Find(id);
        }


        public bool Create(T entity)
        {
            _dbSet.Add(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return Save();

        }

        public bool Save()
        {
            return  _context.SaveChanges() > 0;
        }
    }
}
