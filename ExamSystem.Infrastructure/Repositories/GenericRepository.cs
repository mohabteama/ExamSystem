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
        public async Task<(ICollection<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var totalCount = await _dbSet.CountAsync();

            var items = await _dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();  
        }
        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public T GetByStringId(string id)
        {
            return _dbSet.Find(id);
        }
        public T GetByIntId(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetByName(string name)
        {
            return _dbSet.FirstOrDefault(e => EF.Property<string>(e, "Name") == name);
        }

        public bool Exists(int id)
        {
            return _dbSet.Find(id) != null;
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

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
