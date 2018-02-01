using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Service.A
{
    internal class RegistrationHandler : RegistrationHandlerBase
    {
        private readonly string _registrationUrl;
        private readonly StringContent _serviceInfoContent;

        internal RegistrationHandler(Uri serviceRegistryUrl, Guid serviceId,
                                   string version, Uri serviceUrl)
        {
            //todo: http, https?
            _registrationUrl = $"http://{serviceRegistryUrl}/api/service-instance/{serviceId}/{version}";

            var serviceInstanceDto = new ServiceInstanceDto()
            {
                Url = serviceUrl.ToString()
            };

            var serviceInfoJson = JsonConvert.SerializeObject(serviceInstanceDto);

            _serviceInfoContent = new StringContent(serviceInfoJson, Encoding.UTF8, "application/json");

        }
        protected override void Register()
        {
            using (var client = new HttpClient())//TODO: переиспользовать подключение (private static field)
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //TODO: в статический конструктор
                var unused = client.PutAsync(_registrationUrl, _serviceInfoContent).Result;
            }
        }
    }
}
