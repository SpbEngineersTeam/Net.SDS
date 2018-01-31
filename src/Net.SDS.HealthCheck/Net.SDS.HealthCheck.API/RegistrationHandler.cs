using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Service.A
{
    class RegistrationHandler
    {
        private readonly Uri _serviceRegistryUrl;
        private readonly Guid _serviceId;
        private readonly string _version;
        private readonly Uri _serviceUrl;

        public RegistrationHandler(Uri serviceRegistryUrl, Guid serviceId,
                                   string version, Uri serviceUrl)
        {
            _serviceRegistryUrl = serviceRegistryUrl;
            _serviceId = serviceId;
            _version = version;
            _serviceUrl = serviceUrl;
        }

        internal void Register()
        {
            //todo: http and https
            var uri = $"http://{_serviceRegistryUrl}/api/service-instance/{_serviceId}/{_version}";

            using (var client = new HttpClient())
            {
                var serviceInfo = new ServiceInstanceDto()
                {
                    Url = _serviceUrl.ToString()
                };
                var serviceInfoJson = JsonConvert.SerializeObject(serviceInfo);


                    var unused = client.PutAsync(uri, new StringContent(serviceInfoJson)).Result;
              
            }
        }
    }

    public class ServiceInstanceDto
    {
        public string Url { get; set; }
    }
}
