using System;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents an asynchronous session to the database that
    /// will only be used to read data. The connection to the database
    /// can be terminated by calling <see cref="IAsyncDisposable.DisposeAsync"/>
    /// (or <see cref="IDisposable.Dispose"/>).
    /// </summary>
    public interface IAsyncReadOnlySession : IDisposable, IAsyncDisposable { }
}