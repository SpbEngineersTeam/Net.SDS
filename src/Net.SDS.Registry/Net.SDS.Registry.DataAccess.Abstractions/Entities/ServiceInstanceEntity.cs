using System;
namespace Net.SDS.ServiceDiscovery.Abstractions.Entities
{
    public class ServiceInstanceEntity
    {
        public Guid ServiceId { get; set; }
        public string Version { get; set; }
        public string Uri { get; set; }//TODO: string -> System.Uri?
    }
}
