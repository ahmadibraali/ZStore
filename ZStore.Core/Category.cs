using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZStore.Core
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 to 50 charcters")]
        public string Name { get; set; }
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description length must be between 10 to 200 charcters")]
        public string Description { get; set; }

        public  ICollection<Product> Products { get; set; } =
            new HashSet<Product>();

    }
}
