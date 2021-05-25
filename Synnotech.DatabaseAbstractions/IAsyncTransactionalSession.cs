using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents an asynchronous session to a database that is able to create individual transactions.
    /// The connection to the database can be terminated by calling <see cref="IAsyncDisposable.DisposeAsync"/>.
    /// Changes can be committed by first creating a transaction using <see cref="BeginTransactionAsync"/>
    /// and later committing this transaction.
    /// PLEASE REMEMBER: when your connection to the database always uses a single transaction, please
    /// consider using the <see cref="IAsyncSession"/> interface instead. Avoid nesting of several transactions
    /// as the interfaces in this package do not cover trees of transactions.
    /// </summary>
    public interface IAsyncTransactionalSession : IAsyncReadOnlySession
    {
        /// <summary>
        /// Creates a new transaction. Please ensure not to call this method while another transaction
        /// is still active in your current scope (to avoid nesting transactions).
        /// </summary>
        /// <param name="cancellationToken">The token to cancel this asynchronous operation (optional).</param>
        Task<IAsyncTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}