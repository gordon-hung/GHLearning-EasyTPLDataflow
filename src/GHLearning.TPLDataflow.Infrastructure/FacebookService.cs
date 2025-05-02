using GHLearning.TPLDataflow.Core.Facebook;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging;

namespace GHLearning.TPLDataflow.Infrastructure;

internal class FacebookService(
	ILogger<FacebookService> logger,
	TimeProvider timeProvider) : IFacebookService
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
