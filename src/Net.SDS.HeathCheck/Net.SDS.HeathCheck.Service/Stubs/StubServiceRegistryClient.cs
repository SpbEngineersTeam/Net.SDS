using Net.SDS.HeathCheck.Service.ServiceRegistryApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service.Stubs
{
	/// <summary>
	/// Заглушка клиента реестра сервисов.
	/// </summary>
	public sealed class StubServiceRegistryClient : IServiceRegistryClient
	{
		public StubServiceRegistryClient(IReadOnlyCollection<ServiceDto> serviceDtos)
		{
			_serviceDtos = serviceDtos;
		}

		public async void DeleteServiceAsync(ServiceDto serviceDto)
		{
			Console.WriteLine($"DELETE {serviceDto}.");
			await Task.Delay(_rnd.Next(2000));
		}

		public async Task<IReadOnlyCollection<ServiceDto>> GetServiceDtoOlderThanAsync(int ms)
		{
			await Task.Delay(_rnd.Next(2000));
			return _serviceDtos;
		}

		public async void UpdateOkServiceAsync(ServiceDto serviceDto)
		{
			Console.WriteLine($"UPDATE {serviceDto}.");
			await Task.Delay(_rnd.Next(2000));
		}

		private readonly Random _rnd = new Random();
		private readonly IReadOnlyCollection<ServiceDto> _serviceDtos;
	}
}
