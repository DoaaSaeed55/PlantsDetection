using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class ModelImage
    {
        public ModelImage()
        {
            DetectDiseases = new HashSet<DetectDisease>();
        }

        public int ModelId { get; set; }
        public string? ImagePath{ get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<DetectDisease> DetectDiseases { get; set; }
    }
}
