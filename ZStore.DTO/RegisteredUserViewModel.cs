using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZStore.DTO
{
    public class RegisteredUserViewModel
    {
               
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        [DataType(DataType.Password)]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address Length Must Be Between 5 to 100 char")]
        public string Address { get; set; }
    }
}
