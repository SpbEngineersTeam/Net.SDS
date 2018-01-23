using System;
using System.Net.Http;
using System.Text;
using AutoFixture;
using Net.SDS.ServiceDiscovery.Abstractions.Dto;
using Newtonsoft.Json;

namespace Net.SDS.IntegrationTests
{
    public abstract class ServiceInstanceTestBase : TestServerHolderBase
    {
        protected (ServiceInstanceDto, string, StringContent) CreateModelUrlContent()
        {
            var serviceId = Guid.NewGuid();
            var version = Fixture.Create("version");
            var model = Fixture.Create<ServiceInstanceDto>();

            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            string url = $"/api/service-instance/{serviceId}/{version}";

            return (model, url, content);
        }
    }
}
