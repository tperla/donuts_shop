namespace DO;
public struct Enums
{
    public enum State
    {menager,customer };
    public enum Category { miniDonuts, belgianWaffles, general, bigDonuts, specials, cupcakes, desserts, None };
    public enum Choice { Exit, Product, Order, OrderItem };
    public enum ChoiceOrder { Add, Delete, Update, GetOrder, GetOrders };
    public enum ChoiceOrderItem { Add, Delete, Update, GetOrderItem, GetOrderItems,GetOrderItemByProductAndOrder,viewAllProducts };
    public enum ChoiceProduct { Add, Delete, Update, GetProduct, GetProducts };
}
