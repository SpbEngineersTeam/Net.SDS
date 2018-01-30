namespace Net.SDS.ServiceDiscovery.Abstractions.Dto
{
    //todo: аналог используется в Net.SDS.HealthCheck.API. 
    //Нужно вынести Net.SDS.Registry.Core.Abstractions в отдельный nuget-пакет, 
    //который подключить в Net.SDS.HealthCheck.API
    public class ServiceInstanceDto
    {
        public string Url { get; set; }
    }
}
