using System;
namespace Net.SDS.ServiceDiscovery.Abstractions.Entities
{
    public class ServiceInstanceEntity
    {
        /// <summary>
        /// Возвращает / устанавливает Guid сервиса.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Возвращает / устанавливает версию сервиса.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Возвращает / устанавливает URI сервиса.
        /// </summary>
        public string Uri { get; set; }//TODO: string -> System.Uri?

        /// <summary>
        /// Возвращает / устанавливает описание сервиса.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Возвращает / устанавливает дату регистрации сервиса в реестре.
        /// </summary>
        public DateTimeOffset RegisteredDate { get; set; }

        /// <summary>
        /// Возвращает / устанавливает дату обновления состояния сервиса в реестре.
        /// </summary>
        public DateTimeOffset UpdatedDate { get; set; }

        /// <summary>
        /// Возвращает / устанавливает признак работоспособности сервиса в данный момент.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Возвращает / устанавливает допустимую продолжительность времени неработоспособности сервиса. 
        /// По достижении данного времени сервис будет помечен как неактивный.
        /// </summary>
        public TimeSpan AllowableDowntime { get; set; }

        /// <summary>
        /// Возвращает / устанавливает максимально возможную продолжительность времени неработоспособности сервиса. 
        /// По достижении данного времени сервис будет удалён из реестра.
        /// </summary>
        public TimeSpan MaxDowntime { get; set; }
    }
}
