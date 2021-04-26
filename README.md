# Synnotech.DatabaseAbstractions
*Provides common abstractions for database access in .NET.*

[![Synnotech Logo](synnotech-large-logo.png)](https://www.synnotech.de/)

[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](https://github.com/Synnotech-AG/Synnotech.DatabaseAbstractions/blob/main/LICENSE)
[![NuGet](https://img.shields.io/badge/NuGet-1.1.0-blue.svg?style=for-the-badge)](https://www.nuget.org/packages/Synnotech.DatabaseAbstractions/)

# How to Install

Synnotech.DatabaseAbstractions is compiled against [.NET Standard 2.0 and 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) and thus supports all major plattforms like .NET 5, .NET Core, .NET Framework 4.6.1 or newer, Mono, Xamarin, UWP, or Unity.

Synnotech.DatabaseAbstractions is available as a [NuGet package](https://www.nuget.org/packages/Synnotech.DatabaseAbstractions/) and can be installed via:

- **Package Reference in csproj**: `<PackageReference Include="Synnotech.DatabaseAbstractions" Version="1.1.0" />`
- **dotnet CLI**: `dotnet add package Synnotech.DatabaseAbstractions`
- **Visual Studio Package Manager Console**: `Install-Package Synnotech.DatabaseAbstractions`

# What does Synnotech.DatabaseAbstractions offer you?

This package currently contains two abstractions: the `IAsyncSession` and `ISession` interfaces. Both of them represent the [Unit-of-Work Design Pattern](https://www.martinfowler.com/eaaCatalog/unitOfWork.html). We strongly recommend to use `IAsyncSession` by default as all database I/O should be executed in an asynchronous fashion to avoid threads being blocked during database queries. This is especially bad when you try to scale service apps as incoming requests will usually be handled by executing code on the .NET Thread Pool (e.g. in ASP.NET Core) which in turn will create new threads when it sees that its threads are blocked. With a high number of concurrent requests, you might end up in a situation where your service app responds really slowly because of all the overhead of new threads being created and the constant context switches between them.

However, some data access libraries do not support asynchronous queries. As of April 2021, Oracle e.g. did not override the asynchronous methods of ADO.NET - all calls will always be executed synchronously (even when you call the async APIs, like `DbConnection.OpenAsync`). You can resort to `ISession` in these circumstances.
