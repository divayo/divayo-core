using Divayo.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Divayo.Core.Data.Repositories
{
    public interface IDivayoRepository<T> where T : IBaseEntity
    {
        #region sync
        T Create(T entity);
        T Update(T entity);
        bool Delete(T entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        #endregion

        #region sync
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        #endregion
    }
}
