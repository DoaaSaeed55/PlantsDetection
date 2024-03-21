using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class DetectDisease
    {
        public int Did { get; set; }
        public string? DiseaseType { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ModelId { get; set; }

        public virtual ModelImage? Model { get; set; }
    }
}
