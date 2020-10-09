using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.Extensions
{
    public static class EntityFrameworkExtensions
    {
        private static readonly MethodInfo _configureGlobalFiltersMethodInfo = typeof(EntityFrameworkExtensions).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

        public static void ApplySoftDeletePatternGlobalFilters(this ModelBuilder modelBuilder, DbContext context)
        {
            var entityTypeList = modelBuilder.Model.GetEntityTypes();
            foreach (var entityType in entityTypeList)
            {
                _configureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(context, new object[] { modelBuilder, entityType });
            }
        }

        #region Global Query Filters

        private static void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (entityType.BaseType != null || !ShouldFilterEntity<TEntity>(entityType)) return;

            var filterExpression = CreateFilterExpression<TEntity>();

            if (filterExpression == null) return;

            modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
        }

        private static bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            return typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType);
        }

        private static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> removedFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? removedFilter : CombineExpressions(expression, removedFilter);
            }

            return expression;
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var body = Expression.AndAlso(expression1.Body, expression2.Body);
            return Expression.Lambda<Func<T, bool>>(body, expression1.Parameters[0]);
        }

        #endregion Global Query Filters
    }
}
