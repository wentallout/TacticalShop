using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TacticalShop.Backend.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData] [MaxLength(70)] public string FullName { get; set; }

        [PersonalData] [MaxLength(100)] public string UserAddress { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}