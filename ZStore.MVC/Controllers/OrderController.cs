using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ZStore.Application.Interfaces;
using ZStore.Core;
using ZStore.DTO;

namespace ZStore.MVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderRepository orderRepository;
		private readonly IOrderItemRepository orderItemRepository;
		private readonly IProductRepository productRepository;

		public OrderController(IOrderRepository _orderRepository,
			IOrderItemRepository _orderItemRepository, IProductRepository _productRepository)
		{
			orderRepository = _orderRepository;
			orderItemRepository = _orderItemRepository;
			productRepository = _productRepository;
		}
		public IActionResult Index()
		{
			return View();
		}
		async Task<bool> CheckUserCart()
		{
			List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
			var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);
			string cookieValue;

			if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
			{
				cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
			}
			foreach (var item in cartItems)
			{
				int quantity = await productRepository.GetQuantity(item.Id);
				if (quantity < item.Quantity)
				{
					return false;
				}
			}
			return true;
		}
		public async Task<IActionResult> CreateOrder(decimal totalPrice)
		{
			List<ProductCartViewModel> cartItems = new List<ProductCartViewModel>();
			var userID = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);
			var UID = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
			var _order = new Order()
			{
				OrderDate = DateTime.Now,
				ArrivalDate = DateTime.Now.AddDays(3),
				Address = "sohag",  //User.Identity.Address
				TotalPrice = totalPrice,
				UserId = UID.Value //"3265a9de-b2fb-4dd9-a491-2eb47da5cf88"   //User.Identity.Name //User.Identity.
			};
			if (await CheckUserCart())
			{
				var orderResult = await orderRepository.CreateAsync(_order);
				string cookieValue;



				if (Request.Cookies.TryGetValue(userID.Value, out cookieValue))
				{
					cartItems = JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieValue);
				}
				foreach (var itemOrder in cartItems)
				{
					int quantity = await productRepository.GetQuantity(itemOrder.Id);
					if (itemOrder.Quantity < quantity)
					{
						var _item = new OrderItem()
						{ OrderId = orderResult.Id, Count = itemOrder.Quantity, ProductId = itemOrder.Id };
						bool res = await orderItemRepository.CreateOrderItem(_item);
						bool updated = await productRepository.updateQuantity(itemOrder.Id, itemOrder.Quantity);
					}
					else
					{
						return Json(quantity);
					}
				}
				Response.Cookies.Delete(userID.Value);
			}
			return Json(null);
		}
	}
}
