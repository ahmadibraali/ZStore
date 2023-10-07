using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcommerce.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        [StringLength(150, MinimumLength = 5, ErrorMessage = "Address length must be between 3 to 50 characters")]
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomUser User { get; set; }

    }
}
