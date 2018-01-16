using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutoFixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Net.SDS.ServiceDiscovery.Abstractions.Dto;
using Net.SDS.ServiceDiscovery.API;
using Newtonsoft.Json;
using Xunit;

namespace Net.SDS.IntegrationTests
{
    public class PutServiceInstanceShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;

        public PutServiceInstanceShould()
        {
            _server = new TestServer(
                new WebHostBuilder()
                    .ConfigureServices(services => services.AddAutofac())
                    .UseStartup<Startup>()
            );

            _client = _server.CreateClient();

            _fixture = new Fixture();
        }

        private (ServiceInstanceDto, string, StringContent) CreateModelUrlContent()
        {
            var serviceId = Guid.NewGuid();
            var version = _fixture.Create("version");
            var model = _fixture.Create<ServiceInstanceDto>();

            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            string url = $"/api/service/{serviceId}/{version}";

            return (model, url, content);
        }

        [Fact]
        public async Task ReturnCreatedServiceInstanceInfo()
        {
            //arrange
            var (model, url, content) = CreateModelUrlContent();

            //act
            var response = await _client.PutAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(model.Url, responseString);
        }

        [Fact]
        public async Task ReturnBadRequest_WhenPutServiceInstanceMoreThanOneTime()
        {
            //arrange
            var (model, url, content) = CreateModelUrlContent();

            //act
            var firstTime = await _client.PutAsync(url, content);
            var secondTime = await _client.PutAsync(url, content);
            var responseString = await secondTime.Content.ReadAsStringAsync();

            Assert.Equal(
                HttpStatusCode.BadRequest.ToString(), 
                secondTime.StatusCode.ToString()
            );
        }
    }
}
