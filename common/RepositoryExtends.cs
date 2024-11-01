using EFCore.BulkExtensions;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System.Linq.Expressions;

namespace common
{
    public static class RepositoryExtends
    {
        public static BulkConfig BulkExcludeOnUpdate<T>(this IUnitOfWork unit, Expression<Func<T, object>> keyExpression)
        {
            if (keyExpression == null || unit == null)
                throw new ArgumentNullException(nameof(keyExpression));

            BulkConfig bulkConfig = new();

            var list = keyExpression.GetPropertyNames<T>();
            bulkConfig.PropertiesToExcludeOnUpdate = (List<string>)list;
            return bulkConfig;
        }

        public static void ExcludeOnUpdate<T>(this BulkConfig bulkConfig, Expression<Func<T, object>> keyExpression)
        {
            if (keyExpression == null || bulkConfig == null)
                return;

            var list = keyExpression.GetPropertyNames<T>();
            bulkConfig.PropertiesToExcludeOnUpdate = (List<string>)list;
        }

        public static IList<string> GetPropertyNames<T>(this Expression<Func<T, object>> keyExpression)
        {
            var list = new List<String>();

            if (keyExpression == null)
                return list;

            if (keyExpression.Body is not MemberExpression)
            {
                if (keyExpression.Body is NewExpression ubody)
                    foreach (var arg in ubody.Arguments)
                    {
                        if (arg != null)
                        {
                            list.Add(((MemberExpression)arg).Member.Name);
                        }
                    }
                else
                {
                    list.Add(
                        keyExpression.Body switch
                        {
                            MemberExpression m =>
                                m.Member.Name,
                            UnaryExpression u when u.Operand is MemberExpression m =>
                                m.Member.Name,
                            _ =>
                                throw new NotImplementedException(keyExpression.GetType().ToString())
                        }

                        );
                }
            }

            return list;
        }

        public static IEnumerable<DateTime> EachDay(this DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static async Task<TEntity?> FirstOrDefaultAsync<TEntity>(
           this IRepository<TEntity> repository,
           Expression<Func<TEntity, bool>> predicate)
           where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.SingleResultQuery()
                                      .AndFilter(predicate);
            var result = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);
            return result;
        }

        public static async Task<TEntity?> FirstOrDefaultAsync<TEntity>(
           this IRepository<TEntity> repository,
           Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> orderby, bool desc = false)
   where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.SingleResultQuery()
                                      .AndFilter(predicate);
            if (!desc)
                query.OrderBy(orderby);
            else
                query.OrderByDescending(orderby);

            var result = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);
            return result;
        }

        public static TEntity? FirstOrDefault<TEntity>(
                   this IRepository<TEntity> repository,
                   Expression<Func<TEntity, bool>> predicate)
                   where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.SingleResultQuery()
                                      .AndFilter(predicate);
            var result = repository.FirstOrDefault(query);
            return result;
        }

        public static async Task<IList<T>?> FromSqlAsync<T>(this ISyncRepository<T> repository, string sql, params object[] parameters)
                    where T : class
        {
            if (repository == null || string.IsNullOrEmpty(sql))
                return default;

            var data = await Task.FromResult(repository.FromSql(sql, parameters)).ConfigureAwait(false);
            return data;
        }

        public static async Task<IPagedList<TEntity>> SearchPageAsync<TEntity>(this IRepository<TEntity> repository,
            int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>>? predicate,
            Expression<Func<TEntity, object>>? orderby, bool desc = false)
            where TEntity : class
        {
#pragma warning disable CA1062 // Validar argumentos de métodos públicos
            var query = repository.MultipleResultQuery()
                            .Page(pageIndex, pageSize);
#pragma warning restore CA1062 // Validar argumentos de métodos públicos

            if (predicate != null)
                query.AndFilter(predicate);

            if (orderby != null)
            {
                if (!desc)
                    query.OrderBy(orderby);
                else
                    query.OrderByDescending(orderby);
            }

            var data = await repository.SearchAsync(query)

              .ToPagedListAsync(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount)

              .ConfigureAwait(false);

            return data;
        }

        public static async Task<IList<TEntity>?> SearchAllAsync<TEntity>(
                    this IRepository<TEntity> repository)
            where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.MultipleResultQuery();

            var result = await repository.SearchAsync(query).ConfigureAwait(false);
            return result;
        }

        public static async Task<IList<TEntity>?> SearchAsync<TEntity>(
            this IRepository<TEntity> repository,
                Expression<Func<TEntity, bool>>? predicate = null)
                where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.MultipleResultQuery().AndFilter(predicate);

            var result = await repository.SearchAsync(query).ConfigureAwait(false);
            return result;
        }

        public static async Task<IList<TEntity>?> SearchAsync<TEntity>(
                    this IRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> orderby, bool desc = false)
            where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.MultipleResultQuery().AndFilter(predicate);
            if (!desc)
                query.OrderBy(orderby);
            else
                query.OrderByDescending(orderby);

            var result = await repository.SearchAsync(query).ConfigureAwait(false);
            return result;
        }

        public static async Task<TEntity?> LastOrDefaultAsync<TEntity>(
                       this IRepository<TEntity> repository,
                       Expression<Func<TEntity, bool>> predicate,
                       Expression<Func<TEntity, object>> orderby, bool desc = false)
                       where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.SingleResultQuery()
                                      .AndFilter(predicate);

            if (!desc)
                query.OrderBy(orderby);
            else
                query.OrderByDescending(orderby);

            var result = await repository.LastOrDefaultAsync(query).ConfigureAwait(false);
            return result;
        }

        public static async Task<TEntity?> SingleOrDefaultAsync<TEntity>(
                    this IRepository<TEntity> repository,
            Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (repository == null)
                return default;

            var query = repository.SingleResultQuery()
                                      .AndFilter(predicate);
            var result = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);
            return result;
        }
    }
}
