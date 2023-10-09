using ZStore.Core;


namespace ZStore.Application.Interfaces
{
    public interface IOrderItemRepository:IRepository<OrderItem, int>
    {
		public Task<bool> CreateOrderItem(OrderItem orderItem);

	}
}
