using System;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents a synchronous session to a database that is able to create individual transactions.
    /// The connection to the database can be terminated by calling <see cref="IDisposable.Dispose"/>.
    /// Changes can be committed by first creating a transaction using <see cref="BeginTransaction"/>
    /// and later committing this transaction.
    /// PLEASE REMEMBER: database calls should be performed asynchronously
    /// by default, especially in service apps to avoid blocking threads. Consider using the
    /// <see cref="IAsyncTransactionalSession"/> instead. Furthermore, when your connection to the
    /// database always uses a single transaction, please consider using the <see cref="IAsyncSession"/>
    /// or <see cref="ISession"/> interfaces instead. Avoid nesting of several transactions as the interfaces
    /// in this package do not cover trees of transactions.
    /// </summary>
    public interface ITransactionalSession : IDisposable
    {
        /// <summary>
        /// Creates a new transaction. Please ensure not to call this method while another transaction
        /// is still active in your current scope (to avoid nesting transactions).
        /// </summary>
        ITransaction BeginTransaction();
    }
}