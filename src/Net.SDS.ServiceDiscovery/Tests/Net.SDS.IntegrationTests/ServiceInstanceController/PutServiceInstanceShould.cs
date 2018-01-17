using System.Threading.Tasks;
using Xunit;

namespace Net.SDS.IntegrationTests
{
    public class PutServiceInstanceShould : ServiceInstanceTestBase
    {
        [Fact]
        public async Task ReturnCreatedServiceInstance()
        {
            //arrange
            var (model, location, content) = CreateModelUrlContent();

            //act
            var response = await Client.PutAsync(location, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Contains(model.Url, responseString);
        }

        [Fact]
        public async Task ReturnBadRequest_WhenPutServiceInstanceMoreThanOneTime()
        {
            //arrange
            var (model, url, content) = CreateModelUrlContent();

            //act
            var firstTime = await Client.PutAsync(url, content);
            var secondTime = await Client.PutAsync(url, content);
            var responseString = await secondTime.Content.ReadAsStringAsync();

            //assert
            secondTime.StatusCode.AssertBadRequest();
        }
    }
}
