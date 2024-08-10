using Challenge.Application.Features.Reservations.Commands;
using Challenge.Outbox.PublisherWorker.Options;
using MediatR;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Outbox.PublisherWorker
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IMediator _mediator;
		private readonly WorkerOptions _workerConfig;

		private Timer? _executionTimer;
		private readonly SemaphoreSlim _semaphore = new(1, 1);

		public Worker(ILogger<Worker> logger, IMediator mediator, IOptions<WorkerOptions> options)
		{
			_logger = logger;
			_mediator = mediator;
			_workerConfig = options.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_executionTimer = new Timer(async _ => await ProcessExecutionAsync(stoppingToken), null, TimeSpan.Zero, TimeSpan.FromMinutes(_workerConfig.ExecutionIntervalMinutes));
			await Task.CompletedTask;
		}

		private async Task ProcessExecutionAsync(CancellationToken stoppingToken)
		{
			if (await _semaphore.WaitAsync(0))
			{
				try
				{
					await RunAsync(stoppingToken);
				}
				catch (ValidationException ex)
				{
					_logger.LogWarning(ex, "{Warning}: {Mensaje}", nameof(ValidationException), ex.Message);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "{Excepcion}: {Mensaje}", nameof(Exception), ex.Message);
				}
				finally
				{
					_semaphore.Release();
				}
			}
			else
			{
				_logger.LogInformation("Previous execution is still running, skipping this cycle.");
			}
		}

		private async Task RunAsync(CancellationToken stoppingToken)
		{
			await _mediator.Send(new ProcessReservationsCommand(), stoppingToken).ConfigureAwait(false);
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			if (_executionTimer != null)
			{
				_executionTimer.Change(Timeout.Infinite, 0);
				await _executionTimer.DisposeAsync();
			}
			await base.StopAsync(cancellationToken);
		}
	}
}
