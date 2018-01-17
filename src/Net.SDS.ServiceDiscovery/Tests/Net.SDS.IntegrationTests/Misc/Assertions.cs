using System;
using System.Net;
using Xunit;

namespace Net.SDS.IntegrationTests
{
    public static class Assertions
    {
        public static void AssertNotFound(this HttpStatusCode code)
        {
            Assert.Equal(
                HttpStatusCode.NotFound.ToString(),
                code.ToString()
            );
        }

        public static void AssertBadRequest(this HttpStatusCode code)
        {
            Assert.Equal(
                HttpStatusCode.BadRequest.ToString(),
                code.ToString()
            );
        }

        public static void AssertOk(this HttpStatusCode code)
        {
            Assert.Equal(
                HttpStatusCode.OK.ToString(),
                code.ToString()
            );
        }
    }
}
