using Microsoft.Extensions.AI;
using ProviderAgnosticAi.Api.Models;

namespace ProviderAgnosticAi.Api.Services;

public class ProviderAgnosticChatService : IProviderAgnosticChatService
{
    private readonly IChatClient _chatClient;
    private readonly ILogger<ProviderAgnosticChatService> _logger;

    public ProviderAgnosticChatService(
        IChatClient chatClient,
        ILogger<ProviderAgnosticChatService> logger)
    {
        _chatClient = chatClient;
        _logger = logger;
    }

    public async Task<ChatResponseDto> AskAsync(
        ChatRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.UserMessage))
            {
                return new ChatResponseDto
                {
                    Success = false,
                    ErrorMessage = "User message is required.",
                    Provider = "IChatClient"
                };
            }

            var messages = new List<ChatMessage>
            {
                new(ChatRole.System,
                    "You are a helpful enterprise AI assistant. " +
                    "Explain concepts clearly and professionally."),

                new(ChatRole.User, request.UserMessage)
            };

            ChatResponse response =
                await _chatClient.GetResponseAsync(
                    messages,
                    cancellationToken: cancellationToken);

            return new ChatResponseDto
            {
                Success = true,
                Answer = response.Text,
                Provider = _chatClient.GetType().Name,
                ErrorMessage = null
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Provider-agnostic AI chat failed.");

            return new ChatResponseDto
            {
                Success = false,
                Provider = _chatClient.GetType().Name,
                ErrorMessage = "Unable to generate a response at this time."
            };
        }
    }
}