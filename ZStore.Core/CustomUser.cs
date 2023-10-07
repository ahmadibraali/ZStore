using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;


namespace ZStore.Core
{
    public class CustomUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address length must be between 5 to 100 characters")]
        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; } =
            new HashSet<Order>();
    }
}
