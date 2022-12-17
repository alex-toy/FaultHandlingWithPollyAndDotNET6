using Microsoft.AspNetCore.Mvc;
using Policies;

namespace ResquestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        public readonly IHttpClientFactory _clientFactory;

        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
        {
            _clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }

        // GET: api/<RequestController>
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            //var client = new HttpClient();
            var client = _clientFactory.CreateClient("ImmediateRetryClient");

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
