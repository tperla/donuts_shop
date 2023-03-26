using DalApi;
namespace Dal
{
    sealed internal class DalList : IDal
    {
        public IProduct product { get; } = new Dal.DalProduct();
        public IOrder order { get; } = new Dal.DalOrder();
        public IOrderItem orderItem { get; } = new Dal.DalOrderItem();
        public IUsers users { get; } = new Dal.DalUsers();
        public static IDal Instance { get; } = new DalList();
        private DalList() { }//constractor
    }
}
