namespace GHLearning.TPLDataflow.SharedKernel;

public interface IDataflowHandler
{
	Task HandlerAsync(DataflowMessage message, CancellationToken cancellationToken = default);
}
