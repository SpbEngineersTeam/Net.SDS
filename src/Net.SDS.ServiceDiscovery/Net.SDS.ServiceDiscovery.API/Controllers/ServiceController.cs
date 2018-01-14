using System;
using Microsoft.AspNetCore.Mvc;
using Net.SDS.ServiceDiscovery.Abstractions.Dto;
using Net.SDS.ServiceDiscovery.Abstractions.Services;

namespace Net.SDS.ServiceDiscovery.API.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IRegistryService _registryService;
        public ServiceController(IRegistryService registryService)
        {
            _registryService = registryService;
        }

        [HttpGet("{serviceId:guid}/{version}/url")]
        [Produces(typeof(Uri[]))]
        public IActionResult GetUrls(Guid serviceId, string version)
        {
            var urls = _registryService.GetAvailabelInstances(serviceId, version);

            return urls == null
                ? NotFound($"No services with serviceId {serviceId} and version {version}")
                    : (IActionResult)Ok(urls);
        }

        [HttpPut("{serviceId:guid}/{version}")]
        public IActionResult Put(Guid serviceId, string version, [FromBody] ServiceInstanceDto info)
        {
            var added = _registryService.AddInstance(serviceId, version, info);

            return CreatedAtAction(nameof(GetUrls), added);
        }

        [HttpDelete("{serviceId:guid}/{version}/{url}")]
        public IActionResult Delete(Guid serviceId, string version, string url)
        {
            var deleted = _registryService.DeleteInstances(serviceId, version, url);

            return deleted == null 
                ? NotFound($"No services with serviceId {serviceId}, version {version} and url {url}")
                :(IActionResult) Ok(deleted);
        }
    }
}
