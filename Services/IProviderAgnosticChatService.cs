using ProviderAgnosticAi.Api.Models;

namespace ProviderAgnosticAi.Api.Services;

public interface IProviderAgnosticChatService
{
    Task<ChatResponseDto> AskAsync(
        ChatRequestDto request,
        CancellationToken cancellationToken);
}