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
		private readonly Random _rnd = new Random();
		private readonly IReadOnlyCollection<ServiceDto> _serviceDtos;
		public StubServiceRegistryClient(IReadOnlyCollection<ServiceDto> serviceDtos)
		{
			_serviceDtos = serviceDtos;
		}

		public Task DeleteServiceAsync(ServiceDto serviceDto)
		{
			Console.WriteLine($"DELETE {serviceDto}.");
			return Task.Delay(_rnd.Next(2000));
		}

		public async Task<IReadOnlyCollection<ServiceDto>> GetServiceDtoOlderThanAsync(int ms)
		{
			await Task.Delay(_rnd.Next(2000));

			return _serviceDtos;
		}

		public Task UpdateOkServiceAsync(ServiceDto serviceDto)
		{
			Console.WriteLine($"UPDATE {serviceDto}.");
			return Task.Delay(_rnd.Next(2000));
		}
	}
}
