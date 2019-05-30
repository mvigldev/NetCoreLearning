using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Contract for a class that executes a database transaction on a <see cref="DbContext"/>.
    /// </summary>
    public interface IResilientTransaction
    {
        /// <summary>
        /// Executes the asynchronous operation.
        /// </summary>
        /// <param name="context">The context to create the transaction on.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>
        /// A tasks that executes the operation
        /// </returns>
        Task ExecuteAsync(DbContext context, Func<Task> action);
    }
}
