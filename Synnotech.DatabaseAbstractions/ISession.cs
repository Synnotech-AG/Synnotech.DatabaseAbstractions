using System;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents a synchronous session to a database. The
    /// connection to the database can be terminated by calling
    /// <see cref="IDisposable.Dispose"/>. Changes can be saved or committed
    /// to the database by calling <see cref="SaveChanges"/>.
    /// PLEASE REMEMBER: database calls should be performed asynchronously
    /// by default, especially in service apps to avoid blocking threads.
    /// Consider using the <see cref="IAsyncSession"/> interface instead.
    /// </summary>
    /// <remarks>
    /// Conceptually, a session is identical to a "Unit of Work".
    /// The term "session" is just simpler to use in daily life.
    /// </remarks>
    public interface ISession : IDisposable
    {
        /// <summary>
        /// Writes or commits all changes that occurred during the session to the target database.
        /// </summary>
        void SaveChanges();
    }
}
