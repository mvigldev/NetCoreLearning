using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.Database
{
    /// <summary>
    /// Class which is responsible to use the same database transaction for multiple contexts.
    /// </summary>
    public class ResilientTransaction
    {
        private readonly DbContext _context;
        private ResilientTransaction(DbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Creates a new ResilientTransaction.
        /// </summary>
        /// <param name="context">The context begin transaction.</param>
        /// <returns>The ResilientTransaction created</returns>
        public static ResilientTransaction New(DbContext context) =>
            new ResilientTransaction(context);

        /// <summary>
        /// Executes the asynchronous operation.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A tasks that executes the operation</returns>
        public async Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts 
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();
                }
            });
        }
    }
}
