namespace GHLearning.TPLDataflow.SharedKernel;

public interface ICommunicationService
{
	Task SendMessageAsync(DataflowMessage message, CancellationToken cancellationToken = default);
}
