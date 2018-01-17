using System.Threading.Tasks;
using Xunit;

namespace Net.SDS.IntegrationTests
{
    public class GetServiceInstanceShould : ServiceInstanceTestBase
    {
        [Fact]
        public async Task ReturnServiceInstance_WhenServiceInstaceExists()
        {
            //arrange
            var (model, putLocation, content) = CreateModelUrlContent();
            await Client.PutAsync(putLocation, content);
            var getUrlLication = putLocation + "/url";

            //act
            var response = await Client.GetAsync(getUrlLication);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(model.Url, responseString);
        }

        [Fact]
        public async Task ReturnNotFound_WhenServiceInstaceNotExists()
        {
            //arrange
            var (_, location, _) = CreateModelUrlContent();

            //act
            var response = await Client.GetAsync(location);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.AssertNotFound();
        }
    }
}
