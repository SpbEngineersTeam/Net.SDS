using System.Net;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service
{
	/// <summary>
	/// Интерфейс запроса сервиса по адресу.
	/// </summary>
	public interface IWebRequester
	{
		/// <summary>
		/// Выполняет запрос к адерсу.
		/// </summary>
		/// <param name="url">Целевой адрес запроса.</param>
		/// <returns>Результат обарботки запроса.</returns>
		Task<HttpStatusCode> CheckAsync(string url);
	}
}
