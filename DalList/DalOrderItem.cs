using DalApi;
using DO;
namespace Dal;
using System.Runtime.CompilerServices;
internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// A function to add orderItem, and return the ID
    /// </summary>
    /// <param name="orIt"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  int Add(OrderItem orderItem)//להוסיף חריגה
    {
        if (DataSource._orderItemList.FirstOrDefault(p => p?.ID == orderItem.ID) != null)
            throw new DalAlreadyExsistExeption("OrderItem");
        orderItem.ID = DataSource.config._nextOrderItem;
        //insert new orderItem to array
        DataSource._orderItemList.Add(orderItem);
        return orderItem.ID;
    }
    /// <summary>
    ///delete this orderItem from array
    /// </summary>
    /// <param name="idorderItem"></param>
    /// <exception cref="DO.DalDoesNotExistExeption"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  void Delete(int idorderItem)
    {
        if (DataSource._orderItemList.Exists(x => x?.ID == idorderItem))
        {
            //delete product
            DataSource._orderItemList.Remove(DataSource._orderItemList.Find(x => x?.ID == idorderItem));
            return;
        }

        //if this product does not exist in array
        throw new DO.DalDoesNotExistExeption("this orderItem does not exist");
    }
    /// <summary>
    /// Returns a orderItem by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  OrderItem Get(int id)
    {
        return DataSource._orderItemList.Find(x => x?.ID == id) ??/*if this id does not exist in array*/ throw new DO.DalMissingIdException("this id does not exist");
    }
    /// <summary>
    ///     //return a list/array of all the orderItems that in stock
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource._orderItemList.Where(item => filter(item));
        return DataSource._orderItemList.Select(item => (item));
    }
    /// <summary>
    ///     //Updates the orderItem with new data 
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="DO.DalDoesNotExistExeption"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  void Update(OrderItem orderItem)    
    {
        var p = DataSource._orderItemList.FindIndex(x => x?.ID == orderItem.ID);
        if (p > -1)
        {
            DataSource._orderItemList[p] = orderItem;
            return;
        }
        //if this product does not exist in array
        throw new DO.DalDoesNotExistExeption("this product does not exist");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetbyFilter(Func<OrderItem?, bool>? filter)
    {
        return DataSource._orderItemList.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found ");
    }
}