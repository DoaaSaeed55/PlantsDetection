namespace PlantsDetection.ViewModels
{
    public class ImageModelViewModel
    {
        public int ModelId { get; set; }
        public string? ModelType { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public IFormFile Image { get; set; }
        public virtual User? User { get; set; }
    }
}
