using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZStore.DTO
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 to 50 charcters")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
