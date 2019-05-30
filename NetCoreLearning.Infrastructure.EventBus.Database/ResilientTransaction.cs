using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Class which is responsible to use the same database transaction for multiple contexts.
    /// </summary>
    public class ResilientTransaction : IResilientTransaction
    {
        /// <summary>
        /// Executes the asynchronous operation.
        /// </summary>
        /// <param name="context">The context to create the transaction on.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>
        /// A tasks that executes the operation
        /// </returns>
        public async Task ExecuteAsync(DbContext context, Func<Task> action)
        {
            if (context == null) throw new System.ArgumentNullException(nameof(context));
            if (action == null) throw new System.ArgumentNullException(nameof(action));
            // Use of an EF Core resiliency strategy when using multiple DbContexts 
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();
                }
            });
        }
    }
}
