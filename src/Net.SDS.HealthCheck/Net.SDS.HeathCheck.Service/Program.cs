using Net.SDS.HeathCheck.Service.ServiceRegistryApi;
using Net.SDS.HeathCheck.Service.Stubs;
using System;
using System.Linq;

namespace Net.SDS.HeathCheck.Service
{
	public class Program
	{
		public static void Main()
		{
			var services = new[] {
					new ServiceDto("1", "x_1"),
					new ServiceDto("2", "x_2"),
					new ServiceDto("3", "x_3"),
					new ServiceDto("4", "x_4"),
					new ServiceDto("5", "x_5"),
				};

			var stubWebReq = new StubWebRequester(services.Take(3).Select(s => s.Url));
			var stubSrClient = new StubServiceRegistryClient(services);

			using (var service = new HealthCheckService(stubSrClient, stubWebReq)){

				Console.WriteLine("Start.");
				service.Start();

				Console.Read();

				service.Stop();
				Console.WriteLine("Completed.");
			}
		}
	}
}
