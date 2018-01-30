using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.SDS.HeathCheck.Service.ServiceRegistryApi
{
	/// <summary>
	/// Интерфес - заглушка клиента реестра сервисов.
	/// </summary>
	public interface IServiceRegistryClient
	{
		/// <summary>
		/// Возвращает все сервисы, который обновлялись последний раз больше чем ms миллисекунд назад.
		/// </summary>
		/// <param name="ms">Таймат в миллисекундах, старше которого должна быть дата последнего обновления сервиса.</param>
		/// <returns>Множество сервисов для проверки.</returns>
		Task<IReadOnlyCollection<ServiceDto>> GetServiceDtoOlderThanAsync(int ms);

		/// <summary>
		/// Обновляет информацию о доступности сервиса в реестре.
		/// </summary>
		/// <param name="serviceDto">Данные идентифицирующие доступный сервис.</param>
		/// <returns>Пустая задача.</returns>
		Task UpdateOkServiceAsync(ServiceDto serviceDto);

		/// <summary>
		/// Помечает сервис, в реестре сервисов, как недоступный.
		/// </summary>
		/// <param name="serviceDto">Данные идентифицирующие доступный сервис.</param>
		/// <returns>Пустая задача.</returns>
		Task DeleteServiceAsync(ServiceDto serviceDto);
	}
}
