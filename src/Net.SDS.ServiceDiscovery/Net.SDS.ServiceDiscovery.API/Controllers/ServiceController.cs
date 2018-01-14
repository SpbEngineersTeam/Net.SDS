using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Net.SDS.ServiceRegistry.Abstractions.Dto;
using Net.SDS.ServiceRegistry.Abstractions.Services;

namespace Net.SDS.ServiceRegistry.API.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IRegistryService _registryService;

    

        [HttpGet("{serviceId:guid}/{version}/url")]
        [Produces(typeof(Uri[]))]
        public IActionResult Get(Guid serviceId, string version)
        {
            var instances = _registryService.GetAvailabeInstances(serviceId, version);

            return instances == null ? (IActionResult) NotFound() : Ok(instances);
        }

        [HttpPut("{serviceId:guid}/{version}")]
        public void Put(Guid serviceId, string version, 
                        [FromBody]ServiceInfoDto serviceInfo)
        {
            _registryService.AddAvailabeInstance(serviceId, version, serviceInfo);
        }

        [HttpDelete("{serviceId:guid}/{version}")]
        public void Delete(Guid serviceId, string version)
        {
            var c = HttpContext.Connection.RemoteIpAddress.;
            _registryService.DeleteInstances(serviceId, version, string.Empty);
        }

        [HttpGet]
        public string Get(){
            var c = HttpContext.Connection;
            return string.Empty;
        }
    }
}
