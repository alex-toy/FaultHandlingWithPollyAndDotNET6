using Microsoft.AspNetCore.Mvc;

namespace ResquestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/<RequestController>
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7048/api/Response/75");
            Console.WriteLine($" Response code : {response.StatusCode.ToString()}");

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
