using GHLearning.TPLDataflow.Infrastructure;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace GHLearning.TPLDataflow.InfrastructureTests;
public class TelegramServiceTest
{
	[Fact]
	public async Task SendMessageAsync()
	{
		var fakeLogger = NullLogger<TelegramService>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();

		var sut = new TelegramService(fakeLogger, fakeTimeProvider);

		var cancellationTokenSource = new CancellationTokenSource();

		await sut.SendMessageAsync(
			message: new DataflowMessage(
				Phone: "0912345678",
				Message: "Hello, World!"),
			cancellationToken: cancellationTokenSource.Token);
	}
}
