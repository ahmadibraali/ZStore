using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcommerce.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
