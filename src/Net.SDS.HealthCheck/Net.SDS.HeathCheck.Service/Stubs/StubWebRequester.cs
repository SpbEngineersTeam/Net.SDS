using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service.Stubs
{
	/// <summary>
	/// Заглушка запрашивателя статуса.
	/// </summary>
	public sealed class StubWebRequester : IWebRequester
	{
		public StubWebRequester(IEnumerable<string> urls)
		{
			foreach (var itr in urls) {
				_okServices.Add(itr);
			}
		}

		public Task<HttpStatusCode> CheckAsync(string url)
		{
			return Task.Run(async () => {
				await Task.Delay(_rnd.Next(1500));
				return _okServices.Contains(url) ? HttpStatusCode.OK : HttpStatusCode.NotFound;
			});
		}

		private readonly HashSet<string> _okServices = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		private readonly Random _rnd = new Random();
	}
}
