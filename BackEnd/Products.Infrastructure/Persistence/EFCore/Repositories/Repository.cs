using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Products.Domain.Repository;

namespace Products.Infrastructure.Persistence.EFCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entity;
        public Repository(DbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
            => _entity.Add(entity);

        public void AddRange(IEnumerable<TEntity> entities)
            => _entity.AddRange(entities);

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
            => await _entity.Where(predicate).ToListAsync();

        public async Task<TEntity> Get(int id)
            => await _entity.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAll()
            => await _entity.ToListAsync();

        public void Remove(TEntity entity)
            => _entity.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities)
            => _entity.RemoveRange(entities);

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
            => await _entity.SingleOrDefaultAsync(predicate);
    }
}
