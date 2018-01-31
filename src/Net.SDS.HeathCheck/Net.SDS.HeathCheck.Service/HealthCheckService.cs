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
			_statTask = new Task(()=>ShowStat(token), token, TaskCreationOptions.LongRunning);
		}

		/// <summary>
		/// Запускает сервис.
		/// </summary>
		public void Start()
		{
			_receivingTask.Start();
			_checkingTask.Start();
			_statTask.Start();
		}

		/// <summary>
		/// Останавливает сервис.
		/// </summary>
		public void Stop()
		{
			_cancellationTokenSource.Cancel();
		}

		private async void ShowStat(CancellationToken token)
		{
			while (!token.IsCancellationRequested){
				await Task.Delay(StatTimeout, token);

				Console.WriteLine($"------------ Queue.Count: {_checkedServiceQueue.Count}");
			}
		}

		private async void Process(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				if (!_checkedServiceQueue.TryDequeue(out ServiceDto serviceDto)) {
					await Task.Delay(ServiceCheckPeriod, token);
					continue;
				}

				FireAndForget(()=> CheckService(serviceDto), token);
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
					_checkedServiceQueue.Enqueue(itr);
				}
			}
		}
		
		private async void CheckService(ServiceDto serviceDto)
		{
			var code = await _webRequester.CheckAsync(serviceDto.Url);

			if (code == HttpStatusCode.OK)
			{
				_serviceRegistryClient.UpdateOkServiceAsync(serviceDto);
			}
			else
			{
				_serviceRegistryClient.DeleteServiceAsync(serviceDto);
			}
		}

		private static async void FireAndForget(Action action, CancellationToken token)
		{
			await Task.Run(action, token);
		}

		private const int ServiceCheckPeriod = 1000; //1 s.
		private const int StatTimeout = 5000; // 5 s.
		private readonly IServiceRegistryClient _serviceRegistryClient;
		private readonly IWebRequester _webRequester;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly Task _receivingTask;
		private readonly Task _checkingTask;
		private readonly Task _statTask;
		private readonly ConcurrentQueue<ServiceDto> _checkedServiceQueue = new ConcurrentQueue<ServiceDto>();
	}
}