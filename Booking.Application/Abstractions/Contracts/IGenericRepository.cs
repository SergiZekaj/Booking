using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id);

        Task<List<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
