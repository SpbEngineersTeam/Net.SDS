using System;
using Net.SDS.ServiceDiscovery.Abstractions.Entities;

namespace Net.SDS.ServiceDiscovery.Abstractions.Repositories
{
    public interface IServiceInstanceRepository
    {
        ServiceInstanceEntity Create(Guid serviceId, string version, string url);
        ServiceInstanceEntity Delete(Guid serviceId, string version, string url);
        string[] GetUriByIdVer(Guid serviceId, string version);
        bool IsExistsByVerUrl(string version, string url);
    }
}
