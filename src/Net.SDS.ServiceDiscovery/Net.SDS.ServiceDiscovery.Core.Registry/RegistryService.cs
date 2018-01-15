using System;
using Net.SDS.ServiceDiscovery.Abstractions;
using Net.SDS.ServiceDiscovery.Abstractions.Dto;
using Net.SDS.ServiceDiscovery.Abstractions.Entities;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;
using Net.SDS.ServiceDiscovery.Abstractions.Services;

namespace Net.SDS.ServiceDiscovery.Registry
{
    public class RegistryService : IRegistryService
    {
        private readonly IServiceInstanceRepository serviceInstanceRepository;

        public RegistryService(IServiceInstanceRepository serviceInstanceRepository)
        {
            this.serviceInstanceRepository = serviceInstanceRepository;
        }

        public ServiceInstanceDto AddInstance(Guid serviceId, string version, ServiceInstanceDto info)
        {
            if (serviceInstanceRepository.IsExistsByVerUrl(version, info.Url))
            {
                throw new ServiceInstanceAlreadyExistsException(version, info.Url);
            }

            var entity = serviceInstanceRepository.Create(serviceId, version, info.Url);

            return ToDto(entity);
        }

        public ServiceInstanceDto DeleteInstances(Guid serviceId, string version, string url)
        {
            var entity = serviceInstanceRepository.Delete(serviceId, version, url);

            return entity == null ? null : ToDto(entity);
        }

        public string[] GetAvailabelInstances(Guid serviceId, string version)
        {
            var instances = serviceInstanceRepository.GetUriByIdVer(serviceId, version);

            return instances;
        }

        private ServiceInstanceDto ToDto(ServiceInstanceEntity entity)
        {
            var dto = new ServiceInstanceDto()
            {
                Url = entity.Uri
            };

            return dto;
        }
    }
}
