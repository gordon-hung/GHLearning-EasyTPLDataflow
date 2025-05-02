using GHLearning.TPLDataflow.Core.Facebook;
using GHLearning.TPLDataflow.Core.Line;
using GHLearning.TPLDataflow.Core.Telegram;
using GHLearning.TPLDataflow.Infrastructure;
using GHLearning.TPLDataflow.SharedKernel;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services)
		=> services
		.AddSingleton(TimeProvider.System)
		.AddSingleton<IPhoneUtensil, PhoneUtensil>()
		.AddTransient<IFacebookService, FacebookService>()
		.AddTransient<ILineService, LineService>()
		.AddTransient<ITelegramService, TelegramService>();
}
