using System.Threading.Tasks;
using Xunit;

namespace Net.SDS.IntegrationTests
{
    public class DeleteServiceInstanceShould : ServiceInstanceTestBase
    {
        [Fact]
        public async Task ReturnDeletedServiceInstance()
        {
            //arrange
            var (model, location, content) = CreateModelUrlContent();
            await Client.PutAsync(location, content);

            //act
            var response = await Client.DeleteAsync(location + $"/{model.Url}");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(model.Url, responseString);
        }

        [Fact]
        public async Task ReturnNotFound_WhenDeleteNonexistentServiceInstance()
        {
            //arrange
            var (model, location, _) = CreateModelUrlContent();

            //act
            var response = await Client.DeleteAsync(location + $"/{model.Url}");

            //assert
            response.StatusCode.AssertNotFound();
        }
    }
}
