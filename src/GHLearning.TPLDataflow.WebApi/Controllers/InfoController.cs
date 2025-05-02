using System.Net;

using System.Net.Sockets;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace GHLearning.TPLDataflow.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
	[HttpGet]
	public async Task<object> GetAsync(
		[FromServices] IWebHostEnvironment hostingEnvironment)
	{
		var hostName = Dns.GetHostName();
		var hostEntry = await Dns.GetHostEntryAsync(hostName).ConfigureAwait(false);
		var hostIp = Array.Find(hostEntry.AddressList,
			x => x.AddressFamily == AddressFamily.InterNetwork);

		return new
		{
			Environment.MachineName,
			HostName = hostName,
			HostIp = hostIp?.ToString() ?? string.Empty,
			Environment = hostingEnvironment.EnvironmentName,
			OsVersion = $"{Environment.OSVersion}",
			Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
			ProcessCount = Environment.ProcessorCount
		};
	}
}
