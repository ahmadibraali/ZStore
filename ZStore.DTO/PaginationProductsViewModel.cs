using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZStore.Core;
using static System.Net.Mime.MediaTypeNames;

namespace ZStore.DTO
{
    public class PaginationProductsViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 to 50 characters")]
        public String Name { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }


        public List<ProductImage> Images { get; set; }

    }
}
