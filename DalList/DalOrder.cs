using DalApi;
using DO;
namespace Dal;
using System.Runtime.CompilerServices;

internal class DalOrder:IOrder
{
    /// <summary>
    /// A function to add order, and return the ID
    /// </summary>
    /// <param name="or"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  int Add(Order order)
    {
        if (DataSource._orderList.FirstOrDefault(p => p?.ID == order.ID) != null)
            throw new DalAlreadyExsistExeption("Order");
        order.ID = DataSource.config._nextOrderNumber;
            //insert new order to array
        DataSource._orderList.Add (order);
       
        return order.ID;   
    }
    ///
    ///delete this order from array
    ///
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  void Delete(int idOrder)
    {
        if (DataSource._orderList.Exists(x => x?.ID == idOrder))
        {
            //delete order
            DataSource._orderList.Remove(DataSource._orderList.Find(x => x?.ID == idOrder));
            return;
        }
        //if this order does not exist in array
        throw new DO.DalDoesNotExistExeption("this order does not exist");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetbyFilter(Func<Order?, bool>? filter)
    {
        return DataSource._orderList.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found ");
    }
    /// <summary>
    ///     //Returns a order by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  Order Get(int id)
    {
        return DataSource._orderList.Find(x => x?.ID == id) ??/*if this id does not exist in array*/  throw new DO.DalMissingIdException("this id of order does not exist");
    }
    //return a list/array of all the orders that in stock
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource._orderList.Where(item => filter(item));
        return DataSource._orderList.Select(item => (item));

    }
    /// <summary>
    /// Updates the order with new data
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="DO.DalDoesNotExistExeption"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  void Update(Order order)
    {
        var o = DataSource._orderList.FindIndex(x => x?.ID == order.ID);
        if (o > -1)
        {
            DataSource._orderList[o] = order;
            return;
        }
        //if this order does not exist in array
        throw new DO.DalDoesNotExistExeption("this order does not exist");
    }
}
