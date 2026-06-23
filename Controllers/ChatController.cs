using Microsoft.AspNetCore.Mvc;
using ProviderAgnosticAi.Api.Models;
using ProviderAgnosticAi.Api.Services;

namespace ProviderAgnosticAi.Api.Controllers;

[ApiController]
[Route("api/provider-agnostic-chat")]
public class ChatController : ControllerBase
{
    private readonly IProviderAgnosticChatService _chatService;

    public ChatController(IProviderAgnosticChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> Ask(
        [FromBody] ChatRequestDto request,
        CancellationToken cancellationToken)
    {
        ChatResponseDto response =
            await _chatService.AskAsync(request, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}