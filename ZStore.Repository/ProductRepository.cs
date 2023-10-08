using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZStore.Application.Interfaces;
using ZStore.Core;
using ZStore.Data;
using ZStore.DTO;

namespace ZStore.Repository
{
    public class ProductRepository:Reposit<Product,int>,IProductRepository
    {
        private readonly IMapper mapper;

        public ProductRepository(ZStoreContext context,IMapper _mapper) : base(context) 
        {
            mapper = _mapper;
        }

        public async Task<IQueryable<PaginationProductsViewModel>> GetAllProductPagination(int item, int Pagenumber, int catId)
        {
            var AllProducts = await GetAllAsync();
            IQueryable<PaginationProductsViewModel> paginationProducts;
            if (catId > 0)
            {
                paginationProducts = AllProducts.Where(p => p.CategoryId == catId && p.UnitInStock > 0).Skip(item * (Pagenumber - 1)).Take(item).
               Select(p => new PaginationProductsViewModel
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Price = p.Price,
                   CategoryId = p.CategoryId,
                   Images = p.Images.Select(i => i).ToList()
               });
            }
            else
            {
                paginationProducts = AllProducts.Where(p => p.CategoryId != 0 && p.UnitInStock > 0).Skip(item * (Pagenumber - 1)).Take(item).
                   Select(p => new PaginationProductsViewModel
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Price = p.Price,
                       CategoryId = p.CategoryId,
                       Images = p.Images.Select(i => i).ToList()
                   });
            }
            return paginationProducts;
        }
        public async Task<int> GetQuantity(int productId)
        {
            var product = await GetByIdAsync(productId);
            return product?.UnitInStock ?? 0;
        }

        public async Task<bool> updateQuantity(int productId, int newQuantity)
        {
            var product = await GetByIdAsync(productId);
            product.UnitInStock -= newQuantity;
            bool res = false;
            if (product != null)
            {
                res = await UpdateAsync(product);
                await SaveChangesAsync();
            }

            return (res) ? true : false;
        }
    }
}
