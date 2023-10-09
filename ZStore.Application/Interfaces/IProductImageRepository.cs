using ZStore.Core;


namespace ZStore.Application.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage, int>
    {
		Task<ProductImage> CreateAsync(string name, int pid);
		Task<List<ProductImage>> ProductImages(int pid);
	}
}
