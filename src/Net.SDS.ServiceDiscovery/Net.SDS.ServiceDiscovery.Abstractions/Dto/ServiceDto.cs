using System;
namespace Net.SDS.ServiceDiscovery.Abstractions.Dto
{
    public class ServiceDto
    {
        public Guid ServiceId { get; set; }
        public string[] Instances { get; set; }
    }
}
