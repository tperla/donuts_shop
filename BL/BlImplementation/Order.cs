
using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using IOrder = BlApi.IOrder;
namespace BlImplementation
{
    internal class Order : IOrder
    {
        DalApi.IDal? dal = DalApi.Factory.Get();
        /// <summary>
        /// func for the manager so he can see the order status
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.OrderTracking? FollowOrder(int idOrder)
        {
            try { 
            DO.Order? doOrder;
            try
            {//check if the order in list fron dal
                doOrder = dal!.order.Get(idOrder);
            }
            catch (DO.DalMissingIdException ex)
            {
                throw new BO.BlMissingEntityException("order does not exist", ex);
            }
            // build the traking list
            List<Tuple<DateTime?, string?>> myTracking = new List<Tuple<DateTime?, string?>>();
            //check the current status
            if (doOrder?.OrderDate != null)
            {
                myTracking.Add(new Tuple<DateTime?, string?>((DateTime)doOrder?.OrderDate!, "The order had been creted"));
            }
            if (doOrder?.ShipDate != null)
            {
                myTracking.Add(new Tuple<DateTime?, string?>((DateTime)doOrder?.ShipDate!, "The order had been sent"));
            }
            if (doOrder?.DeliveryrDate != null)
            {
                myTracking.Add(new Tuple<DateTime?, string?>((DateTime)doOrder?.DeliveryrDate!, "The order had been delivered"));
            }
            return new BO.OrderTracking
            {
                Id = idOrder,
                status = OrderStatusByDate(doOrder),
                //status = OrderStatusByDate((DateTime)doOrder?.OrderDate!, (DateTime)doOrder?.ShipDate!, (DateTime)doOrder?.DeliveryrDate!),
                Traking = myTracking
            };
        }
            catch(DO.DalAlreadyExsistExeption ex)
            {
                throw new BO.BlMissingEntityException("order does not exist", ex);
            }
        }
        /// <summary>
        /// Returns the latest order by the katest date
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order OrderEarlier()
        {
            IEnumerable<BO.Order> ordersInLine = from order in GetListedOrders()
                                                 where order.Status != BO.Enums.OrderStatus.Delivered
                                                 select GetOrder(order.ID);

            return ordersInLine.Where(order => GetLatestDate(order) == ordersInLine.Min(_order => GetLatestDate(_order))).FirstOrDefault()!;
        }
        /// <summary>
        /// Returning the latests update of the order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static DateTime? GetLatestDate(BO.Order order) => order.DeliveryrDate is not null ?
    order.DeliveryrDate : order.ShipDate is not null ? order.ShipDate : order.OrderDate;
        /// <summary>
        /// func that show to the menager all the orders
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.OrderForList?> GetListedOrders(Func<BO.OrderForList?, bool>? filter = null)
        {
            var dal_orders = dal!.order.GetAll();
            foreach (var item in dal_orders)
            {
                var amount = dal.orderItem.GetAll(x => { return x?.OrderID == item?.ID; }).Count();
            }
            IEnumerable<OrderForList?> list = from DO.Order? doOrder in dal!.order.GetAll()
                   select new BO.OrderForList
                   {
                       ID = doOrder?.ID ?? throw new NullReferenceException("Missing ID"),
                       CustomerName = doOrder?.CustomerName ?? throw new NullReferenceException("Missing Name"),
                       Status=OrderStatusByDate(doOrder),
                       AmountOfItems = dal.orderItem.GetAll(x => { return x?.OrderID == doOrder?.ID; }).Sum(orderItem => (int)orderItem?.Amount!),//func that sum the all amount
                       TotalPrice = dal.orderItem.GetAll(x => { return x?.OrderID == doOrder?.ID; }).Sum(orderItem => (int)orderItem?.Price! * (int)orderItem?.Amount!)//price per one
                   };
            return filter is null ? list : list.Where(filter);
        }
        /// <summary>
        /// func that help to calculate the corect status
        /// </summary>
        /// <param name="OrderDate"></param>
        /// <param name="ShipDate"></param>
        /// <param name="DeliveryrDate"></param>
        /// <returns></returns>
        private BO.Enums.OrderStatus OrderStatusByDate(DO.Order? item)
        { 
            {
                if (item?.ShipDate == null)
                    return BO.Enums.OrderStatus.Ordered;
                else
                if (item?.DeliveryrDate == null)
                    return BO.Enums.OrderStatus.Shipped;
                else
                    return BO.Enums.OrderStatus.Delivered;
            }
        }
        /// <summary>
        /// func that return order by id
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order? GetOrder(int idOrder)
        {
            if (idOrder < 0)// check the validation
                throw new BO.BlInvalidInputException("invlid ID");    //exeption
            try
            {
                var myOrder = dal!.order.Get(idOrder);
                var myItems = from DO.OrderItem doOrderItem in dal.orderItem.GetAll(x => x?.OrderID == idOrder)
                              select new BO.OrderItem//create list of orderItems by id for this order from BL type
                              {
                                  ID = doOrderItem.ID,
                                  ProductID = doOrderItem.ProductID,
                                  Price = doOrderItem.Price,
                                  Amount = doOrderItem.Amount,
                                  Name = dal.product.Get(doOrderItem.ProductID).Name,
                                  TotalPrice = doOrderItem.Amount * doOrderItem.Price
                              };
                // create list of orders from BL type
                return new BO.Order
                {
                    ID = myOrder.ID,
                    CustomerName = myOrder.CustomerName,
                    CustomerEmail = myOrder.CustomerEmail,
                    CustomerAdress = myOrder.CustomerAdress,
                    OrderDate = myOrder.OrderDate,
                    ShipDate = myOrder.ShipDate,
                    DeliveryrDate = myOrder.DeliveryrDate,
                    Status = OrderStatusByDate(myOrder),
                    Items = myItems,
                    TotalPrice = myItems.Sum(orderItem => orderItem.Price * orderItem.Amount)//price per one
                };
            }
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("dont exist", ex);
            }
        }
        /// <summary>
        /// func that update delivery order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order? OrderDeliveryUpdate(int idOrder)
        {
            //check validation
            if (dal!.order.GetAll().Where(order => (int)order?.ID! == idOrder) == null)
                throw new BO.BlInvalidInputException("dont exist");
            try
            {
                var myOrder = dal.order.Get(idOrder);
                //check the status and update the delivery date for today in case that the order wasnt delivered yet
                if (myOrder.DeliveryrDate == null)
                {
                    if (myOrder.ShipDate == null)
                        throw new BO.BlDidntShipedYet("didnt shiped yet");
                    else
                    {
                        myOrder.DeliveryrDate = DateTime.Now;
                        dal.order.Update((DO.Order)myOrder);
                    }
                }
                else
                    throw new BO.BlAllredyDelivered("allredy delivered");

                return GetOrder(idOrder);
            }
            catch (DO.DalDoesNotExistExeption ex)
            {
                throw new BO.BlMissingEntityException(ex.Message);
            }
        }
        /// <summary>
        /// func that update shipping order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order? OrderShippingUpdate(int idOrder)
        {
            //check validation
            if (dal!.order.GetAll().Where(order => (int)order?.ID! == idOrder) == null)
                throw new BO.BlInvalidInputException("dont exist");
            //check the status and update the shipped date for today in case that the order wasnt shipped yet
            try
            {
                var myOrder = dal.order.Get(idOrder);
                if (myOrder.ShipDate == null)
                {

                    myOrder.ShipDate = DateTime.Now;
                    dal.order.Update((DO.Order)myOrder!);
                }
                else
                    throw new BO.BlAllredyDelivered("allredy shiped");
                return GetOrder(idOrder);
            }
            catch (DO.DalDoesNotExistExeption ex)
            {
                throw new BO.BlMissingEntityException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<OrderForList?> GetListedOrdersByStatus(BO.Enums.OrderStatus? status)
        {
            return GetListedOrders(x => x?.Status == status);
        }
    }
}
