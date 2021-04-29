using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TacticalShop.Domain
{
    public class User : IdentityUser
    {
        public User() : base()
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