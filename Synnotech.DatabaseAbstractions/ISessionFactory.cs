using System.Threading.Tasks;

namespace Synnotech.DatabaseAbstractions
{
    /// <summary>
    /// Represents the abstraction of a factory that creates sessions
    /// and opens them asynchronously. Depending on the abstraction, this might also
    /// involve starting a transaction.
    /// PLEASE CONSIDER: this abstraction should be used with Micro-ORMs or ADO.NET directly where
    /// the client code, not the framework/library handles opening connections and starting transactions.
    /// If your underlying database technology already has a SaveChangesAsync mechanism that involves
    /// an implicit transaction, you probably do not need to use this abstraction, as there is no
    /// need to create and open the session asynchronously.
    /// </summary>
    /// <typeparam name="TSessionAbstraction">
    /// The abstraction that represents a Unit-of-Work for a specific use case.
    /// </typeparam>
    public interface ISessionFactory<TSessionAbstraction>
    {
        /// <summary>
        /// Creates a new session instance and opens the connection to the target database asynchronously. This
        /// usually involves starting a transaction.
        /// </summary>
        ValueTask<TSessionAbstraction> OpenSessionAsync();
    }
}