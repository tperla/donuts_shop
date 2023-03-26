using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
namespace Dal
{
    internal class DoOrderItem : IOrderItem
    {
        readonly string s_rOderItem = "OrderItem";
        /// <summary>
        /// Add function to add an orderItem to the list of orderItems (in the xml files)
        /// </summary>
        /// <param name="orI"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(OrderItem orderI)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            if (listOrderItem.FirstOrDefault(p => p?.ID == orderI.ID) != null)
                throw new DalAlreadyExsistExeption("OrderItem");
            orderI.ID = Config.ReturnNextOrderItemId();
            //insert new orderItem to list
            listOrderItem.Add(orderI);
            Config.UpdateOrderItemId(orderI.ID + 1);
            XMLTools.SaveListToXMLSerializer(listOrderItem, s_rOderItem);
            return orderI.ID;
        }
        /// <summary>
        /// Delete function to remove an orderItem from the list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            if (listOrderItem.Exists(x => x?.ID == id))
            {
                //delete product
                listOrderItem.Remove(listOrderItem.Find(x => x?.ID == id));
                XMLTools.SaveListToXMLSerializer(listOrderItem, s_rOderItem);
                return;
            }
            //if this product does not exist in array
            throw new DO.DalDoesNotExistExeption("this orderItem does not exist");
        }
        /// <summary>
        /// get function to return the orderItem
        /// </summary>
        /// <param name="orI"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public OrderItem Get(int id)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            return listOrderItem.Find(x => x?.ID == id) ??/*if this id does not exist in array*/ throw new DO.DalMissingIdException("this id does not exist");
        }
        /// <summary>
        /// get all function to reteurn all the orderItem
        /// </summary>
        /// <param name="orI"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            if (filter != null)
                return listOrderItem.Where(item => filter(item));
            return listOrderItem.Select(item => (item));
        }
        /// <summary>
        /// get by filter function 
        /// </summary>
        /// <param name="orI"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public OrderItem GetbyFilter(Func<OrderItem?, bool>? filter)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            return listOrderItem.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found ");
        }
        /// <summary>
        /// Update function to update the orderItem
        /// </summary>
        /// <param name="orI"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(OrderItem orderItem)
        {
            List<OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_rOderItem);
            var p = listOrderItem.FindIndex(x => x?.ID == orderItem.ID);
            if (p > -1)
            {
                listOrderItem[p] = orderItem;
                XMLTools.SaveListToXMLSerializer(listOrderItem, s_rOderItem);
                return;
            }
            //if this product does not exist in array
            throw new DO.DalDoesNotExistExeption("this product does not exist");
        }
    }
}
