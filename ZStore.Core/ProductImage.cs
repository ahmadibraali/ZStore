using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ZStore.Core
{
    public class ProductImage
    {
        public int Id { get; set; }
        [RegularExpression(@"\w+\.(jpg|png|gif)")]
        public string Name { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
