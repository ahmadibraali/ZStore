using ZStore.Core;

namespace ZStore.DTO
{
    public class CategoryProductViewModel
    {
        public List<Category> _categories { get; set; }
        public IQueryable<Product> Products { get; set; }
    }
}