namespace ProviderAgnosticAi.Api.Models;

public class ChatResponseDto
{
    public bool Success { get; set; }

    public string Answer { get; set; } = string.Empty;

    public string Provider { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
}