using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Net.SDS.HealthCheck.API
{
	/// <inheritdoc />
	/// <summary>
	/// Класс, сообщающий реестру сервисов о существовании сервиса.
	/// </summary>
	internal sealed class HeartBeat : HeartBeatBase
	{
		internal HeartBeat(
			Uri serviceRegistryUrl, 
			Guid serviceId,
			string version, 
			Uri serviceUrl)
		{
			_registrationUrl = $"{serviceRegistryUrl}/api/service-instance/{serviceId}/{version}";
			
			_serviceInfoContent = new StringContent(
				JsonConvert.SerializeObject(new ServiceInstanceDto { Url = serviceUrl.ToString() }),
				Encoding.UTF8,
				MediaType);

			_client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaType));
		}

		/// <inheritdoc />
		protected override async Task Beat(CancellationToken token)
		{
			await _client.PutAsync(_registrationUrl, _serviceInfoContent, token);
		}

		/// <inheritdoc />
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_client?.Dispose();
		}

		private readonly HttpClient _client;
		private readonly string _registrationUrl;
		private readonly StringContent _serviceInfoContent;
		private const string MediaType = "application/json";
	}
}
