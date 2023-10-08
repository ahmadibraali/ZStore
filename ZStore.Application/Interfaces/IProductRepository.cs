using ZStore.Core;
using ZStore.DTO;

namespace ZStore.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product, int>
    {
        public Task<IQueryable<PaginationProductsViewModel>> GetAllProductPagination(int item, int Pagenumber, int catId);
        Task<int> GetQuantity(int productId);
        Task<bool> updateQuantity(int productId, int newQuantity);
    }
}
