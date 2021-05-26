# Synnotech.DatabaseAbstractions
*Provides common abstractions for database access in .NET.*

[![Synnotech Logo](synnotech-large-logo.png)](https://www.synnotech.de/)

[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](https://github.com/Synnotech-AG/Synnotech.DatabaseAbstractions/blob/main/LICENSE)
[![NuGet](https://img.shields.io/badge/NuGet-1.3.0-blue.svg?style=for-the-badge)](https://www.nuget.org/packages/Synnotech.DatabaseAbstractions/)

# How to Install

Synnotech.DatabaseAbstractions is compiled against [.NET Standard 2.0 and 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) and thus supports all major plattforms like .NET 5, .NET Core, .NET Framework 4.6.1 or newer, Mono, Xamarin, UWP, or Unity.

Synnotech.DatabaseAbstractions is available as a [NuGet package](https://www.nuget.org/packages/Synnotech.DatabaseAbstractions/) and can be installed via:

- **Package Reference in csproj**: `<PackageReference Include="Synnotech.DatabaseAbstractions" Version="2.0.0" />`
- **dotnet CLI**: `dotnet add package Synnotech.DatabaseAbstractions`
- **Visual Studio Package Manager Console**: `Install-Package Synnotech.DatabaseAbstractions`

# What does Synnotech.DatabaseAbstractions offer you?

## IAsyncSession - supporting the Unit-of-Work pattern

This package offers interfaces and abstract base classes for accessing databases. Both the `IAsyncSession` and `ISession` interfaces represent the [Unit-of-Work Design Pattern](https://www.martinfowler.com/eaaCatalog/unitOfWork.html). We strongly recommend to use `IAsyncSession` by default as all database I/O should be executed in an asynchronous fashion to avoid threads being blocked during database queries. This is especially important when you try to scale service apps. Incoming requests will usually be handled by executing code on the .NET Thread Pool (e.g. in ASP.NET Core) which in turn will create new threads when it sees that its threads are blocked. With a high number of concurrent requests, you might end up in a situation where your service app responds really slowly because of all the overhead of new threads being created and the constant context switches between them (thread starvation).

However, some data access libraries do not support asynchronous queries. As of April 2021, Oracle e.g. did not override the asynchronous methods of ADO.NET - all calls will always be executed synchronously (even when you call the async APIs, like `DbConnection.OpenAsync`). You can resort to `ISession` in these circumstances.

There is also an `IAsyncReadOnlySession` interface that derives from both `IDisposable` and `IAsyncDisposable`. It can be used to create abstractions for sessions that only read data.

## Sessions with several transactions

If you need to support individual transactions during a database session, then use the `IAsyncTransactionalSession` (or `ITransactionalSession`) interfaces. Instead of a `SaveChangesAsync` method, you can use this session type to manually begin transactions by calling `BeginTransactionAsync`. You can then save your changes by committing the transaction. Please be aware that you should not nest transaction, i.e. you should not call `BeginTransactionAsync` again while you still have an existing transaction in your current scope.

## ISessionFactory for asynchronously creating and opening sessions 

Some data access libraries, especially Micro-ORMs, require client code to explicitly start a transaction before inserts or updates. If you want to do this asynchronously while hiding the transaction behind an `IAsyncSession`, you can use the `ISessionFactory<T>` interface to create and open session instances asynchronously. The client code might look like this:

```csharp
public sealed class SomeService
{
    public SomeService(ISessionFactory<IMySession> sessionFactory) =>
        SessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));

    // IMySession is a session that is customized for your use case and derives from IAsyncSession
    private ISessionFactory<IMySession> SessionFactory { get; }

    public async Task DoSomethingAsync()
    {
        // The next line will create a new session instance,
        // open the connection to the target database asynchronously.
        // Dependending on the implementation, a transaction might
        // be started asynchronously.
        await using var session = await SessionFactory.OpenSessionAsync();

        // Do something useful here with your session
    }
}
```

Please note that you usually need to use this interface with Micro-ORMs. If your underlying data access library already implements a `SaveChangesAsync` mechanism that involves an implicit transaction (like e.g. RavenDB's `IAsyncDocumentSession` or Entity Framework's `DbContext`), you usually do not need to use the `ISessionFactory` interface.

## Cancellation Tokens

As of version 2.0.0, all async APIs of this package support `CancellationToken` instances to abort async operations. Please be aware that the result of aborting an async operation might lead to different results (e.g. a `OperationCanceledException` or the task returning a result of `-1`) which depends on the implementation of the async methods.