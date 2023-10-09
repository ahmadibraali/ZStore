using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZStore.Application.Interfaces;
using ZStore.Core;
using ZStore.Data;

namespace ZStore.Repository
{
    public class ProductImageRepository :Reposit<ProductImage,int>, IProductImageRepository
    {
		private readonly ZStoreContext context;

		public ProductImageRepository(ZStoreContext _context):base(_context)
        {
			context = _context;
		}

		public async Task<ProductImage> CreateAsync(string name, int pid)
		{
			ProductImage image = new ProductImage();
			image.Name = name;
			image.ProductID= pid;
			var res = await CreateAsync(image);

			return image;
		}

		public Task<List<ProductImage>> ProductImages(int pid)
		{
			return Task.FromResult
				(context.ProductImages.Where(i => i.ProductID == pid).ToList());
		}
	}
}
