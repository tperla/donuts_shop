namespace DalApi
{
    public interface IDal
    {
        IProduct product{ get;}
        IOrder order { get; }
        IOrderItem orderItem { get; }
        IUsers users { get; }
    }
}
