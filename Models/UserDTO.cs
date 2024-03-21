namespace PlantsDetection.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? StreetName { get; set; }
        public string? Phone { get; set; }
    }
}
