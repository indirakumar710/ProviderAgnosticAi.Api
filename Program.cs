using Microsoft.Extensions.AI;
using ProviderAgnosticAi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IChatClient, MockChatClient>();
builder.Services.AddScoped<IProviderAgnosticChatService, ProviderAgnosticChatService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();