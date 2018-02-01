using Net.SDS.HeathCheck.Service.ServiceRegistryApi;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service
{
	/// <inheritdoc />
	/// <summary>
	/// Класс сервиса проверки доступности сервисов.
	/// </summary>
	public sealed class HealthCheckService: IDisposable
	{
		/// <summary>
		/// Создает новый экземпляр класса.
		/// </summary>
		/// <param name="serviceRegistryClient">Клиент реестра сервисов.</param>
		/// /// <param name="webRequester">Исполнитель веб запросов.</param>
		public HealthCheckService(IServiceRegistryClient serviceRegistryClient, IWebRequester webRequester)
		{
			_serviceRegistryClient = serviceRegistryClient ?? throw new ArgumentNullException(nameof(serviceRegistryClient));
			_webRequester = webRequester ?? throw new ArgumentNullException(nameof(webRequester));

			var token = _cancellationTokenSource.Token;

			_receivingTask = new Task(()=>Resive(token), token, TaskCreationOptions.LongRunning);
			_checkingTask = new Task(()=>Process(token), token, TaskCreationOptions.LongRunning);
			_statTask = new Task(()=>ShowStat(token), token, TaskCreationOptions.LongRunning);
		}

		/// <summary>
		/// Запускает сервис.
		/// </summary>
		public void Start()
		{
			if (_disposed){
				throw new ObjectDisposedException(string.Empty);
			}

			_receivingTask.Start();
			_checkingTask.Start();
			_statTask.Start();
		}

		/// <summary>
		/// Останавливает сервис.
		/// </summary>
		public void Stop()
		{
			if (_disposed){
				throw new ObjectDisposedException(string.Empty);
			}

			_cancellationTokenSource.Cancel();
		}

		/// <inheritdoc />
		/// <summary>
		/// Реализует IDisposable.
		/// </summary>
		public void Dispose()
		{
			if (_disposed){
				return;
			}

			_disposed = true;

			if (_cancellationTokenSource != null){
				_cancellationTokenSource.Cancel();
				_cancellationTokenSource.Dispose();
			}

			_checkedServiceSet?.Dispose();
		}
		
		private async void ShowStat(CancellationToken token)
		{
			while (!token.IsCancellationRequested){
				await Task.Delay(StatTimeout, token);

				Console.WriteLine($"------------ Queue.Count: {_checkedServiceSet.Count}");
			}
		}

		private void Process(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				var serviceDto = _checkedServiceSet.Take();
				CheckService(serviceDto);
			}
		}

		private async void Resive(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				var services = await _serviceRegistryClient.GetServiceDtoOlderThanAsync(ServiceCheckPeriod);

				if (!services.Any()) {
					await Task.Delay(ServiceCheckPeriod, token);
					continue;
				}

				foreach (var itr in services) {
					_checkedServiceSet.Add(itr, token);
				}
			}
		}
		
		private async void CheckService(ServiceDto serviceDto)
		{
			var code = await _webRequester.CheckAsync(serviceDto.Url);
			
			if (code == HttpStatusCode.OK) {
				_serviceRegistryClient.UpdateOkServiceAsync(serviceDto);
			} else {
				_serviceRegistryClient.DeleteServiceAsync(serviceDto);
			}
		}

		private const int ServiceCheckPeriod = 1000; //1 s.
		private const int StatTimeout = 5000; // 5 s.
		private readonly IServiceRegistryClient _serviceRegistryClient;
		private readonly IWebRequester _webRequester;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly Task _receivingTask;
		private readonly Task _checkingTask;
		private readonly Task _statTask;
		private readonly BlockingCollection<ServiceDto> _checkedServiceSet = new BlockingCollection<ServiceDto>();
		private bool _disposed;
	}
}