using System;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents an asynchronous session to the database that
    /// will only be used to read data. This usually means that no
    /// transaction is needed for this session. The connection to the database
    /// can be terminated by calling <see cref="IAsyncDisposable.DisposeAsync"/>
    /// (or <see cref="IDisposable.Dispose"/>).
    /// </summary>
    /// <remarks>
    /// Conceptually, a session is identical to a "Unit of Work".
    /// The term "session" is just simpler to use in daily life.
    /// </remarks>
    public interface IAsyncReadOnlySession : IDisposable, IAsyncDisposable { }
}