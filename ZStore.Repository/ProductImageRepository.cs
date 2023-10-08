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
        public ProductImageRepository(ZStoreContext _context):base(_context)
        {
            
        }
        
        
    }
}
