using System.Threading.Tasks.Dataflow;
using GHLearning.TPLDataflow.Core.Facebook;
using GHLearning.TPLDataflow.Core.Line;
using GHLearning.TPLDataflow.Core.Telegram;
using GHLearning.TPLDataflow.SharedKernel;
using Microsoft.Extensions.Logging;

namespace GHLearning.TPLDataflow.Application;

internal class BatchBlockDataflowHandler(
	ILogger<BatchBlockDataflowHandler> logger,
	IPhoneUtensil phoneUtensil,
	IFacebookService facebookService,
	ILineService lineService,
	ITelegramService telegramService) : IDataflowHandler, IDisposable, IAsyncDisposable
{
	private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
	private readonly CancellationTokenSource _cancellationTokenSource = new();
	private BatchBlock<DataflowMessage>? _batchBlock;
	private bool _disposedValue;

	public async Task HandlerAsync(DataflowMessage message, CancellationToken cancellationToken = default)
	{
		if (_batchBlock is null)
		{
			await PrepareAsync(cancellationToken).ConfigureAwait(false);
		}

		_batchBlock?.Post(message);
	}

	public ValueTask DisposeAsync()
	{
		Dispose(true);
		GC.SuppressFinalize(this);

		return ValueTask.CompletedTask;
	}

	public void Dispose()
	{
		// 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_batchBlock?.Complete();
				_cancellationTokenSource.Cancel();
			}

			_cancellationTokenSource.Dispose();
			_disposedValue = true;
		}
	}

	private async Task PrepareAsync(CancellationToken cancellationToken = default)
	{
		using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);

		await _semaphoreSlim.WaitAsync(cts.Token).ConfigureAwait(false);

		try
		{
			if (_batchBlock is not null)
				return;

			var linkOptions = new DataflowLinkOptions()
			{
				PropagateCompletion = true
			};

			async Task SendMessageAsync(ICommunicationService service, DataflowMessage[] messages, CancellationToken cancellationToken)
			{
				try
				{
					foreach (var message in messages)
					{
						await service.SendMessageAsync(message, cancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "發送訊息時發生錯誤");
				}
			}

			// 建立ActionBlock
			ActionBlock<DataflowMessage[]> CreateActionBlock(Func<DataflowMessage[], Task> action)
			{
				return new ActionBlock<DataflowMessage[]>(action);
			}

			var facebookActionBlock = CreateActionBlock((DataflowMessage[] msg) => SendMessageAsync(facebookService, msg, cancellationToken));
			var lineActionBlock = CreateActionBlock((DataflowMessage[] msg) => SendMessageAsync(lineService, msg, cancellationToken));
			var telegramActionBlock = CreateActionBlock((DataflowMessage[] msg) => SendMessageAsync(telegramService, msg, cancellationToken));

			// BroadcastBlock來直接連接
			var broadcastMessageBlock = new BroadcastBlock<DataflowMessage[]>(msg => msg);
			broadcastMessageBlock.LinkTo(facebookActionBlock, linkOptions);
			broadcastMessageBlock.LinkTo(lineActionBlock, linkOptions);
			broadcastMessageBlock.LinkTo(telegramActionBlock, linkOptions);

			// 號碼隱藏處理
			var hidePhoneNumberBlock = new TransformBlock<DataflowMessage[], DataflowMessage[]>(msgs =>
			{
				ArgumentNullException.ThrowIfNull(msgs);

				var transform = new List<DataflowMessage>();
				foreach (var msg in msgs)
				{
					var phone = phoneUtensil.HidePhoneNumber(msg.Phone);
					transform.Add(new DataflowMessage(Phone: phone, Message: msg.Message));
				}

				return [.. transform];
			});
			_batchBlock = new BatchBlock<DataflowMessage>(10);
			_batchBlock.LinkTo(hidePhoneNumberBlock, linkOptions);
			hidePhoneNumberBlock.LinkTo(broadcastMessageBlock, linkOptions);
		}
		finally
		{
			_semaphoreSlim.Release();
		}
	}
}
