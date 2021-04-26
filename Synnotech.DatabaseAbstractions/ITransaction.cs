using System;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents a synchronous transaction that can be committed. The transaction should always be disposed.
    /// A rollback is performed automatically on <see cref="IDisposable.Dispose"/> when commit was not called
    /// beforehand.
    /// PLEASE REMEMBER: database calls should be performed asynchronously
    /// by default, especially in service apps to avoid blocking threads.
    /// Consider using <see cref="IAsyncTransaction"/> instead. 
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Commits the changes made during this transaction to the database.
        /// </summary>
        void Commit();
    }
}