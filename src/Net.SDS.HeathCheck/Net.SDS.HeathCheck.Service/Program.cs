using Net.SDS.HeathCheck.Service.ServiceRegistryApi;
using Net.SDS.HeathCheck.Service.Stubs;
using System;
using System.Linq;
using System.Threading;

namespace Net.SDS.HeathCheck.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceDto[] {
					new ServiceDto("1", "x_1"),
					new ServiceDto("2", "x_2"),
					new ServiceDto("3", "x_3"),
					new ServiceDto("4", "x_4"),
					new ServiceDto("5", "x_5"),
				};

			var stubWebReq = new StubWebRequester(services.Take(3).Select(s => s.Url));
			var stubSrClient = new StubServiceRegistryClient(services);

			var service = new HealthCheckService(stubSrClient, stubWebReq);

			Console.WriteLine("Start.");
			service.Start();

			Thread.Sleep(TimeSpan.FromMinutes(1));

			service.Stop();

			Console.WriteLine("Completed.");
			Console.ReadKey();
		}
	}
}
