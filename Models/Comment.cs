using System;
using System.Collections.Generic;

namespace PlantsDetection.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Users = new HashSet<User>();
            UsersNavigation = new HashSet<User>();
        }

        public int ComId { get; set; }
        public string? Content { get; set; }
        public int? NumOfLikes { get; set; }
        public int? NumOfDisLikes { get; set; }
        public int? NumOfComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<User> UsersNavigation { get; set; }






    }
}
