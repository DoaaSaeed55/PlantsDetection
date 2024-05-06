using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlantsDetection.Identity.Entity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Nationality { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
