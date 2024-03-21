using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class User
    {
        public User()
        {
            ModelImages = new HashSet<ModelImage>();
            Reports = new HashSet<Report>();
            Tokens = new HashSet<Token>();
            Coms = new HashSet<Comment>();
            ComsNavigation = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            PostsNavigation = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public string? Phone { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }
        public string? Token { get; set; }

        public virtual ICollection<ModelImage> ModelImages { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<Comment> Coms { get; set; }
        public virtual ICollection<Comment> ComsNavigation { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Post> PostsNavigation { get; set; }
    }
}
