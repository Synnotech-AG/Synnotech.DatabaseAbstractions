using System.Threading.Tasks;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents the abstraction of a factory that creates sessions
    /// and opens them asynchronously. This usually involves starting a transaction.
    /// PLEASE CONSIDER: this abstraction should be used with Micro-ORMs where
    /// the client code needs to explicitly open a transaction before performing inserts and updates.
    /// If your underlying database technology already has a SaveChangesAsync mechanism that involves
    /// an implicit transaction, you probably do not need to use this abstraction, as there is no
    /// need to create and open the session asynchronously.
    /// </summary>
    /// <typeparam name="TSessionAbstraction">
    /// The abstraction that represents a Unit-of-Work for a specific use case.
    /// It must implement <see cref="IAsyncSession"/>.
    /// </typeparam>
    public interface ISessionFactory<TSessionAbstraction>
        where TSessionAbstraction : IAsyncSession
    {
        /// <summary>
        /// Creates a new session instance and opens the connection to the target database asynchronously. This
        /// usually involves starting a transaction.
        /// </summary>
        Task<TSessionAbstraction> OpenSessionAsync();
    }
}