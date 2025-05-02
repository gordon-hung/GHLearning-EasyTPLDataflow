using GHLearning.TPLDataflow.Infrastructure;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace GHLearning.TPLDataflow.InfrastructureTests;
public class LineServiceTest
{
	[Fact]
	public async Task SendMessageAsync()
	{
		var fakeLogger = NullLogger<LineService>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();

		var sut = new LineService(fakeLogger, fakeTimeProvider);

		var cancellationTokenSource = new CancellationTokenSource();

		await sut.SendMessageAsync(
			message: new DataflowMessage(
				Phone: "0912345678",
				Message: "Hello, World!"),
			cancellationToken: cancellationTokenSource.Token);
	}
}
