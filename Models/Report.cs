using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Describtion { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
