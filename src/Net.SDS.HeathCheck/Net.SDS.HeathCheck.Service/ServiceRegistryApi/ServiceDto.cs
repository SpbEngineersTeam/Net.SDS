namespace Net.SDS.HeathCheck.Service.ServiceRegistryApi
{
	/// <summary>
	/// Предворительная заглушка на ДТО с описанием сервиса.
	/// </summary>
	public sealed class ServiceDto
	{
		/// <summary>
		/// Возвращает GUID сервиса.
		/// </summary>
		public string Guid { get; }

		/// <summary>
		/// Возвращает URL сервиса.
		/// </summary>
		public string Url { get; }

		public override string ToString()
		{
			return $"['GUID': '{Guid}'; 'URL': '{Url}']";
		}

		/// <summary>
		/// Создает новый экземпляр класса.
		/// </summary>
		/// <param name="guid">GUID сервиса.</param>
		/// <param name="url">URL сервиса.</param>
		internal ServiceDto(string guid, string url)
		{
			Guid = guid;
			Url = url;
		}		
	}
}
