using System.Net.Http;

namespace Service.A
{
    class RegistrationHandler
    {
        readonly string registryHostUri;
        readonly RegistrationInfoBase regInfo;

        public RegistrationHandler(string registryHostUri, RegistrationInfoBase regInfo)
        {
            this.registryHostUri = registryHostUri;
            this.regInfo = regInfo;
        }

        public void Register()
        {
            var uri = $"{registryHostUri}/api/service-instance/{regInfo.ServiceId}/{regInfo.ServiceUri}";
           
            using (var client = new HttpClient())
            {
                var unused = client.PostAsync(uri, null).Result;
            }
        }
    }
}
