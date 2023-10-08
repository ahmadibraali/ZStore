using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZStore.DTO
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public String Name { get; set; }
        public decimal Price { get; set; }
        public bool? Status { get; set; }
        public int UnitInStock { get; set; }
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description length must be between 10 to 200 characters")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
