using EFCore.BulkExtensions;

namespace interfaces.infrastructure
{
    public interface IRepositoryBulk

    {
        Task BulkDeleteAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null) where T : class;

        Task BulkInsertAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null) where T : class;

        Task BulkInsertOrUpdateAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null) where T : class;

        Task BulkInsertOrUpdateOrDeleteAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null) where T : class;

        Task BulkUpdateAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null) where T : class;
    }
}
