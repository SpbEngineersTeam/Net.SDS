using System;
using Net.SDS.ServiceRegistry.Abstractions.Dto;

namespace Net.SDS.ServiceRegistry.Abstractions.Services
{
    public interface IRegistryService
    {
        Uri[] GetAvailabeInstances(Guid serviceId, string version);
        void AddAvailabeInstance(Guid serviceId, string version, ServiceInfoDto serviceInfo);
        void DeleteInstances(Guid serviceId, string version, string url);
    }
}
