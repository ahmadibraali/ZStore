using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZStore.Application.Interfaces;

using ZStore.Core;
using ZStore.DTO;
using ZStore.MVC.Models;

namespace ZStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository category;
        private readonly IProductRepository product;

        public HomeController(ICategoryRepository _category, IProductRepository _product )
        {
            category = _category;
            product = _product;
        }

        public async Task<IActionResult> Index()
        {
            // CategoryProductViewModel categoryProductsVm = new CategoryProductViewModel();
            //categoryProductsVm.Products = (IQueryable<Product>)await product.GetAllProductPagination(4, 1, 0);
            // categoryProductsVm._categories = (await category.GetAllAsync()).ToList();
            //return View(categoryProductsVm);
            return View();

		}
        public async Task<IActionResult> LoadMore(int page, int catId)
        {
            return Json(await product.GetAllProductPagination(4, page, catId));
        }
    }
}