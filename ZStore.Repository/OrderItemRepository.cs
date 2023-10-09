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
	public class OrderItemRepository : Reposit<OrderItem, int>, IOrderItemRepository
	{
		public OrderItemRepository(ZStoreContext context) : base(context) { }

		public async Task<bool> CreateOrderItem(OrderItem _orderItem)
		{

			var orderItemCreated = await CreateAsync(_orderItem);
			await SaveChangesAsync();
			return (orderItemCreated != null) ? true : false;
		}

	}
}
