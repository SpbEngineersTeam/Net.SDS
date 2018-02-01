using System;

namespace Net.SDS.HealthCheck.API
{
	/// <summary>
	/// Опции для работы 
	/// </summary>
	public class HealthCheckOptions
	{
		public string ServiceRegistryUrl { get; set; }
		public Guid ServiceId { get; set; }
		public string ServiceUrl { get; set; }
		public string ServiceName { get; set; }
		public string ServiceVersion { get; set; }
	}
}
