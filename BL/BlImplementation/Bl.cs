using BlApi;
namespace BlImplementation
{
    internal class Bl:IBl
    {
        public Bl() { }
        public IOrder? Order { get; } = new BlImplementation.Order();
        public IProduct? Product { get; } = new BlImplementation.Product();
        public ICart? Cart { get; } = new BlImplementation.Cart();
        public IUser? User { get; } = new BlImplementation.User();
    }
}
