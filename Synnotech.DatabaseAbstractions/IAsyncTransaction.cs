using System;
using System.Threading.Tasks;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents an asynchronous transaction that can be committed. The transaction should always be disposed.
    /// A rollback is performed automatically on <see cref="IAsyncDisposable.DisposeAsync"/> when commit was not
    /// called beforehand.
    /// </summary>
    public interface IAsyncTransaction : IAsyncDisposable, IDisposable
    {
        /// <summary>
        /// Commits the changes made during this transaction to the database.
        /// </summary>
        Task CommitAsync();
    }
}