using System.ComponentModel.DataAnnotations;

namespace PlantsDetection.Models
{
    public class TokenRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
