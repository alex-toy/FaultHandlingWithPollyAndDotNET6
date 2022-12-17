using Microsoft.AspNetCore.Mvc;
using Policies;

namespace ResquestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private ClientPolicy _clientPolicy { get; set; }

        public RequestController(ClientPolicy clientPolicy)
        {
            _clientPolicy = clientPolicy;
        }

        // GET: api/<RequestController>
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();

            string serverURL = "https://localhost:7048/api/Response/10";
            Func<Task<HttpResponseMessage>> serverCall = async () => await client.GetAsync(serverURL);

            var response = await client.GetAsync(serverURL);
            //var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(serverCall);
            //var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(serverCall);
            //var response = await _clientPolicy.ExponentialBackOffHttpRetry.ExecuteAsync(serverCall);

            Console.WriteLine($" Response code : {response.StatusCode.ToString()}");

            return response.IsSuccessStatusCode ? Ok() : StatusCode(StatusCodes.Status500InternalServerError); 
        }
    }
}
