using Net.SDS.HeathCheck.Service.ServiceRegistryApi;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service
{
	/// <summary>
	/// Класс сервиса проверки доступности сервисов.
	/// </summary>
	public sealed class HealthCheckService
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
		}

		/// <summary>
		/// Запускает сервис.
		/// </summary>
		public void Start()
		{
			_receivingTask.Start();
		}

		/// <summary>
		/// Останавливает сервис.
		/// </summary>
		public void Stop()
		{
			_cancellationTokenSource.Cancel();
		}

		private async void Process(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				if (!_checkedServiceQueue.TryDequeue(out ServiceDto serviceDto)) {
					await Task.Delay(_serviceCheckPeriod);
				}

				Task.Run(()=> CheckService(serviceDto));
			}
		}

		private async void Resive(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				var services = await _serviceRegistryClient.GetServiceDtoOlderThanAsync(_serviceCheckPeriod);

				if (!services.Any()) {
					await Task.Delay(_serviceCheckPeriod);
					continue;
				}

				foreach (var itr in services) {
					_checkedServiceQueue.Enqueue(itr);
				}
			}
		}
		
		private async void CheckService(ServiceDto serviceDto)
		{
			var code = await _webRequester.CheckAsync(serviceDto.Url);

			if (code == HttpStatusCode.OK) {
				_serviceRegistryClient.UpdateOkServiceAsync(serviceDto);
			}
			else {
				_serviceRegistryClient.DeleteServiceAsync(serviceDto);
			}
		}

		private const int _serviceCheckPeriod = 1000; //1 s.
		private readonly IServiceRegistryClient _serviceRegistryClient;
		private readonly IWebRequester _webRequester;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly Task _receivingTask;
		private readonly Task _checkingTask;
		private ConcurrentQueue<ServiceDto> _checkedServiceQueue = new ConcurrentQueue<ServiceDto>();
	}
}
