using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ZStore.Core;
using ZStore.DTO;

namespace ZStore.MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ShoppingCart()
        {
            List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
            var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);


            string cookieValue;

            if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
            {
                cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
            }
            return View(cartItems);
        }
        [HttpPost]
        public IActionResult AddToCart(string product, int quantity)
        {
            
            List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
            var product1 = JsonConvert.DeserializeObject<PaginationProductsViewModel>(product);
            CookieOptions options = new CookieOptions();
            string cookieValue;
            var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);

            if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
            {
                cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
            }
            ProductCartViewModel newItem;
            var found = cartItems.FirstOrDefault(p => p.Id == product1.Id);
            if (found != null)
            {
                cartItems.Remove(found);
                found.Quantity += quantity;
                cartItems.Add(found);
            }
            else
            {
                newItem = new ProductCartViewModel
                {
                    Id = product1.Id,
                    Name = product1.Name,
                    Price = product1.Price,
                    Image = product1.Images[0],
                    Quantity = quantity
                };
                cartItems.Add(newItem);

            }

            string res = JsonConvert.SerializeObject(cartItems);
            Response.Cookies.Append(userID.Value, res);
            return Json(cartItems.Count);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
            var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);


            string cookieValue;
            if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
            {
                cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
            }
            var found = cartItems.FirstOrDefault(p => p.Id == Id);
            if (found != null)
            {
                var removed = cartItems.Remove(found);
            }
            string res = JsonConvert.SerializeObject(cartItems);
            Response.Cookies.Delete(userID.Value);
            Response.Cookies.Append(userID.Value, res);
            return RedirectToAction("ShoppingCart");
        }
        public IActionResult Update(int Id, int quantity)
        {
            List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
            var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);


            string cookieValue;
            if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
            {
                cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
            }
            var userCart = cartItems.FirstOrDefault(p => p.Id == Id);
            int idx = cartItems.IndexOf(userCart);
            if (idx != -1)
            {
                cartItems[idx].Quantity = quantity;
            }
            string res = JsonConvert.SerializeObject(cartItems);
            Response.Cookies.Delete(userID.Value);
            Response.Cookies.Append(userID.Value, res);
            return Json(cartItems[idx]);
        }
        public IActionResult GetCookieItemCount()
        {
            List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
            var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);


            string cookieValue;
            if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
            {
                cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
            }
            return Json(cartItems.Count);
        }




    }
}
