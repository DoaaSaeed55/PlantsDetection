using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlaskAPIController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public FlaskAPIController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //[HttpGet("GetProcessedImages")]
        //public async Task<IActionResult> GetProcessedImages()
        //{
        //    var client = _clientFactory.CreateClient("FlaskAPI");
        //    var response = await client.GetAsync("/api/images/Models/get");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        return Ok(result);
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}

        //[HttpPost("ProcessImage")]
        //public async Task<IActionResult> ProcessImage()
        //{
        //    // Assume image path or image bytes are received somehow (e.g., from a form file)
        //    // Here we just demonstrate the process
        //    var client = _clientFactory.CreateClient("FlaskAPI");
        //    var imageData = new MultipartFormDataContent();
        //    // Add image content
        //    // imageData.Add(new ByteArrayContent(imageBytes), "image", "filename.jpg");

        //    var response = await client.PostAsync("/api/images/Models/process", imageData);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        return Ok(result);
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}

        [HttpPost("ProcessImage")]
        public async Task<IActionResult> ProcessImage(IFormFile file)
        {
            // Send the image to Flask API
            var response = await SendImageToFlaskAPI(file);

            if (response.IsSuccessStatusCode)
            {
                // Receive the processed image result
                var result = await response.Content.ReadAsStringAsync();
                // Return the processed image result in the response
                return Ok(result);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        private async Task<HttpResponseMessage> SendImageToFlaskAPI(IFormFile file)
        {
            var client = _clientFactory.CreateClient("FlaskAPI");
            var imageData = new MultipartFormDataContent();

            // Add image content
            using (var stream = file.OpenReadStream())
            {
                imageData.Add(new StreamContent(stream), "image", file.FileName);
            }

            // Send the request to Flask API
            var response = await client.PostAsync("/api/images/Models/process", imageData);
            return response;
        }

    }
}
