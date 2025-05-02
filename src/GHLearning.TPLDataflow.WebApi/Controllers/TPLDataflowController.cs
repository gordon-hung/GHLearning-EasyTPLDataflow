using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace GHLearning.TPLDataflow.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TPLDataflowController : ControllerBase
{
	[HttpPost("Broadcast-Block")]
	public Task BroadcastBlockAsync(
		[FromServices] IServiceProvider serviceProvider,
		[FromBody] DataflowMessage message)
	{
		var dataflowHandler = serviceProvider.GetKeyedService<IDataflowHandler>(Dataflows.BroadcastBlock) ?? throw new KeyNotFoundException(nameof(Dataflows.BroadcastBlock));
		return dataflowHandler.HandlerAsync(message, HttpContext.RequestAborted);
	}

	[HttpPost("Batch-Block")]
	public Task BatchBlockAsync(
		[FromServices] IServiceProvider serviceProvider,
		[FromBody] DataflowMessage message)
	{
		var dataflowHandler = serviceProvider.GetKeyedService<IDataflowHandler>(Dataflows.BatchBlock) ?? throw new KeyNotFoundException(nameof(Dataflows.BatchBlock));
		return dataflowHandler.HandlerAsync(message, HttpContext.RequestAborted);
	}

	[HttpPost("Batch-Trigger-Block")]
	public Task BatchTriggerBlockAsync(
		[FromServices] IServiceProvider serviceProvider,
		[FromBody] DataflowMessage message)
	{
		var dataflowHandler = serviceProvider.GetKeyedService<IDataflowHandler>(Dataflows.BatchTimerBlock) ?? throw new KeyNotFoundException(nameof(Dataflows.BatchBlock));
		return dataflowHandler.HandlerAsync(message, HttpContext.RequestAborted);
	}

	[HttpPost("Batch-RX-Block")]
	public Task BatchRXBlockAsync(
		[FromServices] IServiceProvider serviceProvider,
		[FromBody] DataflowMessage message)
	{
		var dataflowHandler = serviceProvider.GetKeyedService<IDataflowHandler>(Dataflows.BatchRXBlock) ?? throw new KeyNotFoundException(nameof(Dataflows.BatchRXBlock));
		return dataflowHandler.HandlerAsync(message, HttpContext.RequestAborted);
	}
}
