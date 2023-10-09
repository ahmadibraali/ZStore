using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZStore.DTO;

namespace ZStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ViewProducts(string _product)
        {
            var product = JsonConvert.DeserializeObject<PaginationProductsViewModel>(_product);
            return View(product);
        }
    }
}
