using GHLearning.TPLDataflow.Core.Telegram;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging;

namespace GHLearning.TPLDataflow.Infrastructure;

internal class TelegramService(
	ILogger<TelegramService> logger,
	TimeProvider timeProvider) : ITelegramService
{
	public Task SendMessageAsync(DataflowMessage message, CancellationToken cancellationToken = default)
	{
		logger.LogInformation("{logInformation}", new
		{
			LogAt = timeProvider.GetUtcNow(),
			Message = message
		});

		return Task.CompletedTask;
	}
}
