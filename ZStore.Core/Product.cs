using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace ZStore.Core
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 to 50 charcters")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitInStock { get; set; }
        public bool Status { get; set; }
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description length must be between 10 to 200 charcters")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public  ICollection<ProductImage> Images { get; set; } =
            new HashSet<ProductImage>();
    }
}