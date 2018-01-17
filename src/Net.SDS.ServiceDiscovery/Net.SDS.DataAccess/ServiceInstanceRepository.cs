using System;
using System.Collections.Generic;
using System.Linq;
using Net.SDS.ServiceDiscovery.Abstractions.Entities;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;

namespace Net.SDS.ServiceDiscovery.DataAccess
{
    internal class ServiceInstanceRepository : IServiceInstanceRepository
    {
        List<ServiceInstanceEntity> _storage = new List<ServiceInstanceEntity>();

        public ServiceInstanceEntity Create(Guid serviceId, string version, string url)
        {
            var entity = CreateEntity(serviceId, version, url);
            _storage.Add(entity);
            return entity;
        }

        public ServiceInstanceEntity Delete(Guid serviceId, string version, string url)
        {
            var toDetele = _storage.FirstOrDefault(x => ByIdVerUrl(serviceId, version, url, x));

            if (toDetele != null)
            {
                _storage.Remove(toDetele);
            }

            return toDetele;
        }

        public string[] GetUriByIdVer(Guid serviceId, string version)
        {
            var urls = _storage.Where(ByIdVer(serviceId, version)).Select(e => e.Uri).ToArray();
            return urls.Any() ? urls : null;
        }

        private static bool ByIdVerUrl(Guid serviceId, string version, string url, ServiceInstanceEntity x)
        {
            return x.ServiceId.Equals(serviceId) && string.Equals(x.Uri, url, StringComparison.OrdinalIgnoreCase) && x.Version == version;
        }

        private static ServiceInstanceEntity CreateEntity(Guid serviceId, string version, string url)
        {
            return new ServiceInstanceEntity()
            {
                Uri = url,
                ServiceId = serviceId,
                Version = version
            };
        }

        private static Func<ServiceInstanceEntity, bool> ByIdVer(Guid serviceId, string version)
        {
            return x => x.ServiceId == serviceId && x.Version == version;
        }

        public bool IsExistsByVerUrl(string version, string url)
        {
            return _storage.Any(x => x.Version == version && x.Uri == url);
        }
    }
}
