using EFCore.BulkExtensions;
using EntityFrameworkCore.UnitOfWork;
using interfaces.infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.DataBase
{
    public class BaseUnitOfWork<T>(T context) : UnitOfWork<T>(context), IRepositoryBulk
          where T : DbContext
    {
        public async Task BulkDeleteAsync<TE>(IEnumerable<TE> entities, BulkConfig? bulkConfig = null) where TE : class
        {
            await DbContext.BulkDeleteAsync<TE>(entities, bulkConfig).ConfigureAwait(true);
        }

        public async Task BulkInsertAsync<TE>(IEnumerable<TE> entities, BulkConfig? bulkConfig = null) where TE : class
        {
            await DbContext.BulkInsertAsync<TE>(entities, bulkConfig).ConfigureAwait(true);
        }

        public async Task BulkInsertOrUpdateAsync<TE>(IEnumerable<TE> entities, BulkConfig? bulkConfig = null) where TE : class
        {
            await DbContext.BulkInsertOrUpdateAsync<TE>(entities, bulkConfig).ConfigureAwait(true);
        }

        public async Task BulkInsertOrUpdateOrDeleteAsync<TE>(IEnumerable<TE> entities, BulkConfig? bulkConfig = null) where TE : class
        {
            await DbContext.BulkInsertOrUpdateOrDeleteAsync<TE>(entities, bulkConfig).ConfigureAwait(true);
        }

        public async Task BulkUpdateAsync<TE>(IEnumerable<TE> entities, BulkConfig? bulkConfig = null) where TE : class
        {
            await DbContext.BulkUpdateAsync<TE>(entities, bulkConfig).ConfigureAwait(true);
        }

    }
}
