using System;
namespace Net.SDS.ServiceDiscovery.Abstractions
{
    public class ServiceInstanceAlreadyExistsException:Exception
    {
        public ServiceInstanceAlreadyExistsException(string version, string url)
            : base ($"Service instance with version {version} and url {url} already exist")
        {
        }
    }
}
