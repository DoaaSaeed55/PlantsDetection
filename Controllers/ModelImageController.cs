using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantsDetection.ViewModels;
using System.IO;
using System;
using Tensorflow;
using static Tensorflow.Binding;
using System.Drawing;
using TensorFlow;
namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PlantsDetectionContext _context;
        public ModelImageController(IWebHostEnvironment webHostEnvironment, PlantsDetectionContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PostImageForModel([FromForm]ImageModelViewModel imageModel)
        {
            string filePath;
            string imagePath;
            try
            {
                if (imageModel == null || imageModel.Image == null || imageModel.Image?.Length == 0)
                {
                    return BadRequest(ModelState);
                }
                string folder = Path.Combine(_webHostEnvironment.ContentRootPath, "ModelImages");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageModel?.Image?.FileName;
                 imagePath = Path.Combine(folder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageModel?.Image?.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {

                throw;
            }


            // todo : create model  for adding to DB
            //var Modelimage = new ModelImage
            //{
            //   ModelType= imagePath,
            //   CreatedAt= DateTime.Now,
            //   UpdatedAt= DateTime.Now,
               
            //};
            //// Add ImageData to database
            //_context.ModelImages.Add(Modelimage);
            //await _context.SaveChangesAsync();


            //try
            //{
            //    // Apply the AI model 
            //    // Load the TensorFlow model
            //    var model = TFModel.LoadModel(Path.Combine(_webHostEnvironment.ContentRootPath, "AIModels\\tomato-leaf-disease-98-accuracy.h5"));

            //    // Load and preprocess the image
            //    string imagePath = filePath;
            //    var image = LoadImage(imagePath);
            //    var preprocessedImage = PreprocessImage(image);

            //    // Perform prediction
            //    var prediction = model.Predict(preprocessedImage);

            //    // Get the predicted class
            //    var predictedClass = GetPredictedClass(prediction);

            //    Console.WriteLine($"Predicted class: {predictedClass}");

            //    //string modelPath = "path/to/your/model.h5";
            //    //string imagePath = "path/to/your/image.jpg";
            //    //int imageWidth = 256;
            //    //int imageHeight = 256;

            //    //// Load the model
            //    //var model = keras.models.load_model(modelPath);

            //    //// Load and preprocess the image
            //    //var image = LoadImage(imagePath, imageWidth, imageHeight);

            //    //// Perform inference
            //    //var prediction = Predict(model, image);

            //    //// Output prediction
            //    //Console.WriteLine($"Prediction: {prediction}");

            //    //// Cleanup
            //    //keras.backend.clear_session();




            //    return Ok();
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status405MethodNotAllowed,"failed to run the AI model");
            //}

            try
            {
                // Path to the image file you want to send
                
                // Create an HttpClient instance
                using (var client = new HttpClient())
                {
                    // Create a new MultipartFormDataContent instance to hold the form data
                    using (var formData = new MultipartFormDataContent())
                    {
                        // Read the image file as bytes
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                        // Create a ByteArrayContent from the image bytes
                        var imageContent = new ByteArrayContent(imageBytes);

                        // Add the image content to the form data with the key 'image'
                        formData.Add(imageContent, "image", "image.jpg");

                        try
                        {
                            // Send the POST request to the Flask endpoint
                            var response = await client.PostAsync("http://127.0.0.1:5000/api/process_image", formData);

                            // Check if the request was successful
                            if (response.IsSuccessStatusCode)
                            {
                                // Read the response content as a string
                                string responseContent = await response.Content.ReadAsStringAsync();

                                // Output the response
                                Console.WriteLine(responseContent);

                                return Ok(responseContent.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception: {ex.Message}");
                        }
                    }
                }

            }
            catch(Exception)
            {
                  return StatusCode(StatusCodes.Status405MethodNotAllowed,"failed to run the AI model");

            }
            return StatusCode(StatusCodes.Status405MethodNotAllowed, "failed to run the AI model");
            //static float[] LoadImage(string imagePath, int width, int height)
            //{
            //    // Load image
            //    var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            //    var tensor = Image.DecodeJpeg(imageBytes, channels: 3);
            //    var resizedTensor = Image.ResizeBilinear(tensor, new Tensor(width), new Tensor(height));

            //    // Convert tensor to float array
            //    var image = resizedTensor.numpy();
            //    var imageData = (float[])image;

            //    return imageData;
            //}

            //static int Predict(TensorFlow.keras.models.Model model, float[] image)
            //{
            //    // Reshape image to match model input shape
            //    var input = np.array(image).reshape(1, 256, 256, 3);

            //    // Perform prediction
            //    var prediction = model.Predict(input);

            //    // Extract predicted class (assuming it's a classification task)
            //    var predictedClass = np.argmax(prediction, axis: 1).GetData<int>()[0];

            //    return predictedClass;
            //}

            static Bitmap LoadImage(string path)
            {
                return new Bitmap(path);
            }

            static float[] PreprocessImage(Bitmap image)
            {
                // Resize the image to match the input size of the model
                var resizedImage = new Bitmap(image, new Size(256, 256));

                // Convert the image to float array and normalize pixel values
                var inputValues = new float[256, 256, 3];
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    for (int x = 0; x < resizedImage.Width; x++)
                    {
                        var pixel = resizedImage.GetPixel(x, y);
                        inputValues[x, y, 0] = pixel.R / 255.0f;
                        inputValues[x, y, 1] = pixel.G / 255.0f;
                        inputValues[x, y, 2] = pixel.B / 255.0f;
                    }
                }

                // Flatten the array
                return inputValues.Cast<float>().ToArray();
            }

            static int GetPredictedClass(float[] prediction)
            {
                // Get the index of the class with the highest probability
                return Array.IndexOf(prediction, prediction.Max());
            }
        }

        public class TFModel
        {
            private TFGraph graph;
            private TFSession session;

            private TFModel(TFGraph graph, TFSession session)
            {
                this.graph = graph;
                this.session = session;
            }

            public static TFModel LoadModel(string modelPath)
            {
                try
                {
                    var graph = new TFGraph();
                    var session = new TFSession(graph);
                    byte[] model = System.IO.File.ReadAllBytes(modelPath);
                    graph.Import(model);
                    return new TFModel(graph, session);
                }
                catch (Exception)
                {

                    throw;
                }
               
            }

            public float[] Predict(float[] input)
            {
                var tensor = TFTensor.FromBuffer(new TFShape(1, 256, 256, 3), input, 0, input.Length);
                var runner = session.GetRunner();
                runner.AddInput(graph["input_1"][0], tensor);
                runner.Fetch(graph["dense_2/Softmax"][0]);
                var output = runner.Run();
                return ((float[][])output[0].GetValue(jagged: true))[0];
            }

        }


    }
}
