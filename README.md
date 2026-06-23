# Using Microsoft.Extensions.AI to Build Provider-Agnostic AI Applications in .NET

This project demonstrates how to build an ASP.NET Core API using Microsoft.Extensions.AI abstractions.

## Key Idea

Instead of coupling the application directly to one AI provider SDK, this project uses `IChatClient`.

## Local Demo

For local development, the project uses:

```csharp
builder.Services.AddSingleton<IChatClient, MockChatClient>();
