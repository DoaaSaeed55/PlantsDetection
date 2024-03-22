using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantsDetection.ViewModels;
using System;
using TensorFlow;
using System.Drawing;
using System.IO;
namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ModelImageController(IWebHostEnvironment webHostEnvironment)
        {
                _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> PostImageForModel(ImageModelViewModel model)
        {
            if (model == null || model.Image == null || model.Image?.Length == 0)
            {
                return BadRequest(ModelState);
            }
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "ModelImages");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model?.Image?.FileName;
            string filePath = Path.Combine(folder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model?.Image?.CopyToAsync(stream);
            }

            // todo : create model  for adding to DB
            try
            {
                // Apply the AI model 



                var modelPath = Path.Combine(_webHostEnvironment.WebRootPath, "tomato-leaf-disease-98-accuracy.h5");  // Provide the path to your TensorFlow model
                var graph = new TFGraph();
                var aiModel = new TFSession(graph);
                graph.Import(new TFBuffer(System.IO.File.ReadAllBytes(modelPath)));
                byte[]? imageData = null;
                var bitmap = new Bitmap(filePath);
                
                // Convert bitmap to byte array (assuming 3-channel RGB image)
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                var imageInputNodeName = "image_input"; // Replace with the actual name of your image input node
                var auxInputNodeName = "aux_input"; // Replace with the actual name of your auxiliary input node

                // Perform inference or other operations with the model
                // Example:
                var tensor = aiModel.GetRunner()
                    .AddInput(graph[imageInputNodeName][0], imageData) // Assuming imageData is a byte array representing the image
                  //.AddInput(graph[auxInputNodeName][0], auxInput) // Assuming auxInput is your auxiliary input data
                  .Run(); // Adjust input node names accordingly
                var result = ((float[,])tensor).GetValue();
                Console.WriteLine(result);

                // Close the session when done
                aiModel.Dispose();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed,"failed to run the AI model");
            }

          

           
        }
    }
}
