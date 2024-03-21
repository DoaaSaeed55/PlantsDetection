using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class Token
    {
        public int Id { get; set; }
        public string? Token1 { get; set; }
        public string? ClientName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
