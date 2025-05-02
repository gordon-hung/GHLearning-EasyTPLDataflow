using GHLearning.TPLDataflow.Core.Line;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging;

namespace GHLearning.TPLDataflow.Infrastructure;

internal class LineService(
	ILogger<LineService> logger,
	TimeProvider timeProvider) : ILineService
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
