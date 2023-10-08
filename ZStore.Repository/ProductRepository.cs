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
    public class ProductRepository:Reposit<Product,int>,IProductRepository
    {
        public ProductRepository(ZStoreContext context) : base(context) { }
        
    }
}
