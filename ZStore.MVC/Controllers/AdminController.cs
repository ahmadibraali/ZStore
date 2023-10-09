using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ZStore.Application.Interfaces;
using ZStore.Core;

namespace ZStore.MVC.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IProductRepository productRepository;
		private readonly IProductImageRepository productImageRepository;
		private readonly IWebHostEnvironment webHostEnvironment;

		public AdminController(ICategoryRepository _categoryRepository,IProductRepository _productRepository
			,IProductImageRepository _productImageRepository ,IWebHostEnvironment _webHostEnvironment)
        {
			categoryRepository = _categoryRepository;
			productRepository = _productRepository;
			productImageRepository = _productImageRepository;
			webHostEnvironment = _webHostEnvironment;
		}
		public async Task<IActionResult> Category()
		{
			List<Category> categories = (await categoryRepository.GetAllAsync()).ToList();
			
			return View(categories);
		}
		public async Task<IActionResult> Product()
		{
			List<Product> products = (await productRepository.GetAllAsync()).ToList();
			return View(products);
		}
		public async Task<IActionResult> AddProduct()
		{
			List<Category> categories = (await categoryRepository.GetAllAsync()).ToList();
			ViewData["categories"] = categories;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddProduct(Product _product,
												  IFormFile image)
		{
			if (ModelState.IsValid)
			{

				string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
				string filname = new Guid().ToString() + "_" + image.FileName;
				string fullPath = Path.Combine(uploadPath, filname);

				using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				var result = await productRepository.CreateAsync(_product);
				await productImageRepository.CreateAsync(filname, result.Id);

				if (result != null)

					return RedirectToAction("Product");
			}
			List<Category> categories = (await categoryRepository.GetAllAsync()).ToList();
			ViewData["categories"]= categories;
			return View(_product);
		}
		public async Task<IActionResult> EditProduct(int id)
		{
			var productModel = await productRepository.GetByIdAsync(id);
			List<Category> categories = (await categoryRepository.GetAllAsync()).ToList();
			productModel.Images = await productImageRepository.ProductImages(id);

			ViewData["categories"] = categories;
			return View(productModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditProduct(Product _productEdited)
		{
			if (ModelState.IsValid)
			{


				var result = await productRepository.UpdateAsync(_productEdited);
				if (result)
					return RedirectToAction("Product");
			}

			List<Category> categories =( await categoryRepository.GetAllAsync()).ToList();
			ViewData["categories"] = categories;
			return View(_productEdited);
		}
		public async Task<IActionResult> DeleteProduct(int id)
		{
			await productRepository.DeleteByIdAsync(id);
			return RedirectToAction("Product");
		}
        public  IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category cat = await categoryRepository.CreateAsync(categoryModel);
                return Redirect("Category");
            }
            return View(categoryModel);
        }
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category categoryModel)
        {
            if (ModelState.IsValid)
            {
                var res = await categoryRepository.UpdateAsync(categoryModel);
                return RedirectToAction("category");
            }
            return View(categoryModel);
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            var result = await categoryRepository.DeleteByIdAsync(id);
            return RedirectToAction("category");
        }



    }
}
