namespace BO
{
    public struct Enums
    {
        public enum Category { miniDonuts, belgianWaffles, general, bigDonuts, specials, cupcakes, desserts,   None };
        public enum State
        { menager, customer };
        public enum OrderStatus { Ordered, Shipped, Delivered, None }
        public enum Choice { Exit, Product, Order, Cart };
        public enum ChoiceOrder { GetAllOrders, GetOrder, OrderDeliveryUpdate, OrderOfTracking, ShippingUpdate, UpdateOrder };
        public enum ChoiceCart{ AddToCart, MakeAnOrder, UpdateStockInCart };
        public enum ChoiceProduct { Add, Delete,  GetProduct, GetProducts,GetProductForList, Update};
    }
}
