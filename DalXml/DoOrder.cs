using DalApi;
using DO;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
namespace Dal
{
    internal class DoOrder : IOrder
    {
        readonly string s_orders = "Order";
        /// <summary>
        /// Add function to add an order to the list of orders (in the xml files)
        /// </summary>
        /// <param name="Or"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Order order)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (listOrder.FirstOrDefault(p => p?.ID == order.ID) != null)
                throw new DalAlreadyExsistExeption("Order");
            order.ID = Config.ReturnNextOrderId();
            //insert new order to array
            listOrder.Add(order);
            Config.UpdateOrderId(order.ID + 1);
            XMLTools.SaveListToXMLSerializer(listOrder, s_orders);
            return order.ID;
        }
        /// <summary>
        /// Delete function to remove an order from the list
        /// </summary>
        /// <param name="ID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (listOrder.Exists(x => x?.ID == id))
            {
                //delete order
                listOrder.Remove((listOrder.Find(x => x?.ID == id)));
                XMLTools.SaveListToXMLSerializer(listOrder, s_orders);
                return;
            }
            //if this order does not exist in array
            throw new DO.DalDoesNotExistExeption("this order does not exist");
        }
        /// <summary>
        /// returns an order from the xml file according to the recieved id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order Get(int id)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            return listOrder.Find(x => x?.ID == id) ??/*if this id does not exist in array*/  throw new DO.DalMissingIdException("this id of order does not exist");
        }
        /// <summary>
        /// returns all orders from the xml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (filter != null)
                return listOrder.Where(item => filter(item));
            return listOrder.Select(item => (item));
        }
        /// <summary>
        /// returns an order from the xml by filter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order GetbyFilter(Func<Order?, bool>? filter)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            return listOrder.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found ");
        }
        /// <summary>
        /// Update function to update the order
        /// </summary>
        /// <param name="order"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Order order)
        {
            List<Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            var o = listOrder.FindIndex(x => x?.ID == order.ID);
            if (o > -1)
            {
                listOrder[o] = order;
                XMLTools.SaveListToXMLSerializer(listOrder, s_orders);
                return;
            }
            //if this order does not exist in array
            throw new DO.DalDoesNotExistExeption("this order does not exist");
        }
    }
}
