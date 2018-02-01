using System;
using Net.SDS.ServiceDiscovery.Abstractions.Dto;

namespace Net.SDS.ServiceDiscovery.Abstractions.Services
{
    public interface IRegistryService
    {
        string[] GetAvailabelInstances(Guid serviceId, string version);
        ServiceInstanceDto AddInstance(Guid serviceId, string version, ServiceInstanceDto serviceInstance);
        ServiceInstanceDto DeleteInstances(Guid serviceId, string version, string url);
    }
}
