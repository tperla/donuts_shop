using BO;
namespace BlApi
{
    public interface IOrder
    {
     //public void UpdateOrderByManager();
        /// <summary>
        /// show to the menagger all the orders
        /// </summary>
        /// <returns></returns>
        ///
     public IEnumerable<OrderForList?> GetListedOrders(Func<BO.OrderForList?, bool>? filter = null);
        /// <summary>
        /// return order by id
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
     public Order? GetOrder(int idOrder);
        /// <summary>
        /// update shipping order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
      public Order? OrderShippingUpdate(int idOrder);
        /// <summary>
        /// update delivery order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
      public Order? OrderDeliveryUpdate(int idOrder);
        /// <summary>
        /// func for the manneger so he can see the order status
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        public OrderTracking? FollowOrder(int idOrder);
        public IEnumerable<OrderForList?> GetListedOrdersByStatus(BO.Enums.OrderStatus? status);
        public BO.Order OrderEarlier();
    }
}
