using Divayo.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divayo.Core.Data.Repositories
{
    public class EFRepository<T> : IDivayoRepository<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            var result = _dbContext.SaveChanges();
            return result == 1;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result == 1;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.Where(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
        }

        public T GetById(Guid id)
        {
            return _dbSet.SingleOrDefault(x => x.Id.Equals(id) && !x.IsDeleted);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
