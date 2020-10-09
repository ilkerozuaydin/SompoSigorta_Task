using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<int> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Added;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = IsSoftDeleteOperation(entity) ? EntityState.Modified : EntityState.Deleted;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? await context.Set<TEntity>().ToListAsync(): await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<int> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        private bool IsSoftDeleteOperation(TEntity entity)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entity.GetType()))
            {
                entity.GetType().GetProperty(nameof(ISoftDelete.IsDeleted)).SetValue(entity, true);
                return true;
            }
            return false;
        }
    }
}
