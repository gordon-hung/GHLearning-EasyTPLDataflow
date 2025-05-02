using GHLearning.TPLDataflow.Application;
using GHLearning.TPLDataflow.SharedKernel;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(
		this IServiceCollection services)
		=> services
		.AddKeyedSingleton<IDataflowHandler, BroadcastBlockDataflowHandler>(Dataflows.BroadcastBlock)
		.AddKeyedSingleton<IDataflowHandler, BatchBlockDataflowHandler>(Dataflows.BatchBlock)
		.AddKeyedSingleton<IDataflowHandler, BatchTimerBlockDataflowHandler>(Dataflows.BatchTimerBlock)
		.AddKeyedSingleton<IDataflowHandler, BatchRXBlockDataflowHandler>(Dataflows.BatchRXBlock);
}
