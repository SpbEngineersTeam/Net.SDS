using Microsoft.AspNetCore.Mvc;

namespace Net.SDS.HealthCheck.API
{
	/// <inheritdoc />
	/// <summary>
	/// Controller provide Health check API from service for a SDS.
	/// </summary>
        [Route("api/health-check")]
        public class HealthCheckController: Controller
        {
		/// <summary>
		/// Return Ok if servic is available.
		/// </summary>
		/// <returns>Ok</returns>
		[HttpGet]
		public IActionResult Get()
		{
			return Ok();
		}

		/// <summary>
		/// Return OK and some additianal information. 
		/// </summary>
		/// <remarks>You can change content of additional information.</remarks>
		/// <param name="flag">flag for operation overloading.</param>
		/// <returns>Ok and some additional information.</returns>
		[HttpGet("{flag}")]
		public IActionResult Get(int flag)
		{
			return Ok(new { AdditionalInfo = "AdditionalInfo" });
		}	
        }
}