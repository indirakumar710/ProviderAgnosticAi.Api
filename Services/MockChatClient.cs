using Microsoft.Extensions.AI;

namespace ProviderAgnosticAi.Api.Services;

public class MockChatClient : IChatClient
{
    public Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var userMessage = messages
            .LastOrDefault(m => m.Role == ChatRole.User)?
            .Text ?? string.Empty;

        string answer;

        if (userMessage.Contains("provider", StringComparison.OrdinalIgnoreCase))
        {
            answer =
                "Provider-agnostic AI design means your application depends on an abstraction like IChatClient, " +
                "instead of directly depending on one AI provider SDK.";
        }
        else if (userMessage.Contains("Microsoft.Extensions.AI", StringComparison.OrdinalIgnoreCase))
        {
            answer =
                "Microsoft.Extensions.AI provides common abstractions such as IChatClient and IEmbeddingGenerator " +
                "so .NET applications can integrate AI services using familiar dependency injection patterns.";
        }
        else if (userMessage.Contains("azure", StringComparison.OrdinalIgnoreCase))
        {
            answer =
                "Azure OpenAI can be used behind the same IChatClient abstraction when a deployment and quota are available.";
        }
        else
        {
            answer =
                "This is a local mock response using IChatClient. The controller depends on Microsoft.Extensions.AI abstractions, " +
                "so the implementation can later be replaced with Azure OpenAI, OpenAI, or another compatible provider.";
        }

        var response = new ChatResponse(
            new ChatMessage(ChatRole.Assistant, answer));

        return Task.FromResult(response);
    }

    public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException(
            "Streaming is not implemented in the mock client for this demo.");
    }

    public object? GetService(Type serviceType, object? serviceKey = null)
    {
        return null;
    }

    public void Dispose()
    {
    }
}