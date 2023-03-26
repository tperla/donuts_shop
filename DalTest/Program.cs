using DO;
using System.Diagnostics;
using static DO.Enums;
internal class Project
{

    //static private DalOrder TestOrder = new DalOrder();
    //static private DalOrderItem TestOrderItem = new DalOrderItem();
    //static private DalProduct TestProduct = new DalProduct();
  
   static DalApi.IDal? dal = DalApi.Factory.Get();


    #region switchToProducts
    static void switchProduct()
    {
        Console.WriteLine(@"Product: 0-Add
        1-Delete
        2-Update
        3-GetProduct 
        4-GetProducts
        -1-Exit ");
        ChoiceProduct choice;
        int num,num1;
        if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
        num = num1;
        choice = (ChoiceProduct)num;
        while (num >= 0)
        {
            int myId, stock;
            double price;
            switch (choice)
            {
                case ChoiceProduct.Add:
                    Console.WriteLine("Enter the product details for a new product");
                    Product product = new Product();
                    Console.WriteLine("Enter the  name of the product");
                    product.Name = Console.ReadLine();
                    Console.WriteLine("Enter the  ID of the product");
                    if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        product.ID = myId;
                   Console.WriteLine("Enter the  price of the product");
                    if (double.TryParse(Console.ReadLine(), out price) == false) throw new Exception("incorrect price");
                    product.Price = price;
                    Console.WriteLine("Enter the  category of the product");
                    Console.WriteLine("Choose from the following options:");
                    Console.WriteLine("miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials");
                    Category c;
                    string? input = Console.ReadLine();
                    bool check = Category.TryParse(input, out c);
                    if (!check)
                        throw new Exception("you press a wrong Ctegory");
                    product.Category = c;
                    Console.WriteLine("Enter the  stock of the product");
                    if (int.TryParse(Console.ReadLine(), out stock) == false) throw new Exception("incorrect stock");
                    product.InStock = stock;
                    try
                    {
                        Console.WriteLine("the id of the product");
                        Console.WriteLine(dal?.product.Add(product));
                       
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceProduct.Delete:
                    Console.WriteLine("enter the id of the product that you want to delete");
                    if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                    int id = myId;
                    try
                    {

                        dal?.product.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceProduct.Update:
                    Console.WriteLine("Enter the product details that you want to update ");
                    Product productUpdate = new Product();
                    Console.WriteLine("Enter the  ID of the product");
                    if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                    productUpdate.ID = myId;
                    //print the product before update
                    Console.WriteLine(dal?.product.GetProduct(productUpdate.ID));
                    //insert values to updateProduct
                    Console.WriteLine("Enter the  name of the product");
                    productUpdate.Name = Console.ReadLine();
                    Console.WriteLine("Enter the  price of the product");
                    if (double.TryParse(Console.ReadLine(), out price) == false) throw new Exception("incorrect price");
                    productUpdate.Price = price;
                    Console.WriteLine("Enter the  category of the product");
                    Category cUpdate;
                    string? inputUpdate = Console.ReadLine();
                    bool checkUpdate = Category.TryParse(inputUpdate, out cUpdate);
                    if (!checkUpdate)
                        throw new Exception("you press a wrong Ctegory");
                    productUpdate.Category = cUpdate;
                    Console.WriteLine("Enter the  stock of the product");
                    if (int.TryParse(Console.ReadLine(), out stock) == false) throw new Exception("incorrect stock");
                    productUpdate.InStock = stock;
                    try
                    {
                        dal?.product.Update( productUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceProduct.GetProduct:
                    Console.WriteLine("enter id of product that you want to search");
                    if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                    int idGetP = myId;
                    try
                    {
                        Console.WriteLine("the id of the product");
                        Console.WriteLine(dal?.product.GetProduct(idGetP));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceProduct.GetProducts:
                    IEnumerable<Product?> newProduct = dal?.product?.GetAll()!;
                    foreach (Product? item in newProduct)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    Console.WriteLine("you press wrong number");
                    break;
            }
            Console.WriteLine(@"Product: 0-Add
        1-Delete
        2-Update
        3-GetProduct 
        4-GetProducts
        -1-Exit ");
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (ChoiceProduct)num;
        }
    }
    #endregion
    #region switchToOrders
    static void switchOrder()
    {
        Console.WriteLine(@"Order: 0-Add
        1-Delete
        2-Update
        3-GetOrder
        4-GetOrders
        -1-Exit ");
        ChoiceOrder choice;
        int num ,num1,idO;
        if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
        num = num1;
        choice = (ChoiceOrder)num;
        while (num >= 0)
        {
            switch (choice)
            {
                case ChoiceOrder.Add:
                    Console.WriteLine("Enter the order details for a new order");
                    Order order = new Order();
                    Console.WriteLine("Enter your  name ");
                    order.CustomerName = Console.ReadLine();
                    Console.WriteLine("Enter your adress ");
                    order.CustomerAdress = Console.ReadLine();
                    Console.WriteLine("Enter your email ");
                    order.CustomerEmail = Console.ReadLine();
                    order.OrderDate = DateTime.Now;
                    order.DeliveryrDate = order.OrderDate?.AddHours(2.5);
                    order.ShipDate = order.OrderDate?.AddHours(1);
                    try
                    {
                        Console.WriteLine("the id of the order");
                        Console.WriteLine(dal?.order.Add(order));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.Delete:
                    Console.WriteLine("enter the id of the order that you want to delete");
                    if (int.TryParse(Console.ReadLine(), out idO) == false) throw new Exception("incorrect id");
                    int id = idO;
                    try
                    {
                        dal?.order.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.Update:
                    Console.WriteLine("Enter the order details that you want to update ");
                    Order orderUpdate = new Order();
                    Console.WriteLine("Enter the ID of order ");
                    if (int.TryParse(Console.ReadLine(), out idO) == false) throw new Exception("incorrect id");
                    orderUpdate.ID = idO;
                    //print the order before update
                    Console.WriteLine(dal?.order.Get(orderUpdate.ID));
                    //insert values to updateOrder
                    Console.WriteLine("Enter your name ");
                    orderUpdate.CustomerName = Console.ReadLine();
                    Console.WriteLine("Enter your adress ");
                    orderUpdate.CustomerAdress = Console.ReadLine();
                    Console.WriteLine("Enter your email ");
                    orderUpdate.CustomerEmail = Console.ReadLine();
                    //date
                    //orderUpdate.OrderDate = DateTime.Now;
                    //update the date of te order
                    DateTime orderDate;
                    Console.WriteLine("enter the date of the order");
                    DateTime.TryParse(Console.ReadLine(), out orderDate);
                    orderUpdate.OrderDate = orderDate;
                    orderUpdate.DeliveryrDate = orderUpdate.OrderDate?.AddHours(2.5);
                    orderUpdate.ShipDate = orderUpdate.OrderDate?.AddHours(1);
                    try
                    {
                        dal?.order.Update(orderUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.GetOrder:
                    Console.WriteLine("enter id of order that you want to search");
                    if (int.TryParse(Console.ReadLine(), out idO) == false) throw new Exception("incorrect id");
                    int idGet = idO;
                    try
                    {
                        Console.WriteLine("the id of the order");
                        Console.WriteLine(dal?.order.Get(idGet));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.GetOrders:
                    IEnumerable<Order?> newOrders = dal?.order?.GetAll()!;
                    foreach (Order? item in newOrders)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine(@"Order: 0-Add
        1-Delete
        2-Update
        3-GetOrder
        4-GetOrders
        -1-Exit ");
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (ChoiceOrder)num;
        }
    }
    #endregion
    #region switchToOrderItems
    static void switchOrderItem()
    {
        Console.WriteLine(@"OrderItem: 0-Add
        1-Delete
        2-Update
        3-GetOrderItem
        4-GetOrderItems
        5- get order item by product and order
        6-view all product in order
        -1-Exit ");
        ChoiceOrderItem choice;
        int num,num1,idItem,idProduct,idOrde,priceItem,amountItem;
        if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
        num = num1;
        choice = (ChoiceOrderItem)num;
        while (num >= 0)
        {
            switch (choice)
            {
                case ChoiceOrderItem.Add:
                    Console.WriteLine("Enter the orderItem details for a new orderItem");
                    OrderItem orderItem = new OrderItem();
                    Console.WriteLine("Enter the order ID ");
                    if (int.TryParse(Console.ReadLine(), out idOrde) == false) throw new Exception("incorrect id");
                    orderItem.OrderID = idOrde;
                    Console.WriteLine("Enter the price ");
                    if (int.TryParse(Console.ReadLine(), out priceItem) == false) throw new Exception("incorrect price");
                    orderItem.Price = priceItem;
                    Console.WriteLine("Enter the product ID ");
                    if (int.TryParse(Console.ReadLine(), out idProduct) == false) throw new Exception("incorrect idProduct");
                    orderItem.ProductID = idProduct;
                    Console.WriteLine("Enter the amount of the products ");
                    if (int.TryParse(Console.ReadLine(), out amountItem) == false) throw new Exception("incorrect amount");
                    orderItem.Amount = amountItem;
                    dal?.orderItem.Add(orderItem);
                    try
                    {
                        Console.WriteLine("the id of orderItem");
                        Console.WriteLine(dal?.orderItem.Add(orderItem));
                    }
                    catch (Exception str)
                    {
                        
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.Delete:
                    Console.WriteLine("enter the id of the orderItem that you want to delete");
                    if (int.TryParse(Console.ReadLine(), out idItem) == false) throw new Exception("incorrect id");
                    int id = idItem;
                    try
                    {
                        dal?.orderItem.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.Update:
                    Console.WriteLine("Enter the orderItem details that you want to update ");
                    OrderItem orderItemUpdate = new OrderItem();
                    Console.WriteLine("Enter the orderItem ID ");
                    if (int.TryParse(Console.ReadLine(), out idItem) == false) throw new Exception("incorrect id");
                    orderItemUpdate.ID = idItem;
                    //print the orderItem before update
                    Console.WriteLine(dal?.orderItem.Get(orderItemUpdate.ID));
                    //insert values to updateOrderItem
                    Console.WriteLine("Enter the order ID ");
                    if (int.TryParse(Console.ReadLine(), out idOrde) == false) throw new Exception("incorrect idOrder");
                    orderItemUpdate.OrderID = idOrde;
                    Console.WriteLine("Enter the price ");
                    if (int.TryParse(Console.ReadLine(), out priceItem) == false) throw new Exception("incorrect price");
                    orderItemUpdate.Price = priceItem;
                    Console.WriteLine("Enter the product ID ");
                    if (int.TryParse(Console.ReadLine(), out idProduct) == false) throw new Exception("incorrect idProduct");
                    orderItemUpdate.ProductID = idProduct;
                    Console.WriteLine("Enter the amount of the products");
                    if (int.TryParse(Console.ReadLine(), out amountItem) == false) throw new Exception("incorrect amount");
                    orderItemUpdate.Amount = amountItem;
                    try
                    {
                        dal?.orderItem.Update(orderItemUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.GetOrderItem:
                    Console.WriteLine("enter id of orderItem that you want to search");
                    if (int.TryParse(Console.ReadLine(), out idItem) == false) throw new Exception("incorrect id");
                    int idGetItem = idItem;
                    try
                    {
                        Console.WriteLine(dal?.orderItem.Get(idGetItem));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.GetOrderItems:
                    IEnumerable<OrderItem?> newOrderItems = dal?.orderItem?.GetAll()!;
                    foreach (OrderItem? item in newOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                  
                    break;
                case ChoiceOrderItem.GetOrderItemByProductAndOrder:
                    Console.WriteLine("enter id of your order");
                    int idO = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter id of  product");
                    int idP = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dal?.orderItem.GetbyFilter(x => x?.ProductID == idP && x?.OrderID == idO));
                    break;
                case ChoiceOrderItem.viewAllProducts:
                    Console.WriteLine("enter id of your order");
                    int idOrder = Convert.ToInt32(Console.ReadLine());
                    IEnumerable<OrderItem> orderI = (IEnumerable<OrderItem>)dal?.orderItem?.GetAll(x => x?.ID == idOrder)!;
                    foreach (OrderItem item in orderI)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine(@"OrderItem: 0-Add
        1-Delete
        2-Update
        3-GetOrderItem
        4-GetOrderItems
        5- get order item by product and order
        6-view all product in order
        -1-Exit ");
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (ChoiceOrderItem)num;
        }
    }
    #endregion

    static void Main(string[] arg)
    {

        Choice choice;
        int num,num1;
        Console.WriteLine(@"Shop Menu: 0-Exit
        1-Product
        2-Order 
        3-OrderItem ");
        if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
        num = num1;
        choice = (Choice)num;
        //You can only choose a number between 0 and 3
        while (num <= 3 && num >= 0)
        {
            switch (choice)
            {
                case Choice.Exit:
                    break;
                //Call the function that performs actions on the products (delete, add, update, etc.)
                case Choice.Product:
                    switchProduct();
                    break;
                //Call the function that performs actions on the orders (delete, add, update, etc.)
                case Choice.Order:
                    switchOrder();
                    break;
                //Call the function that performs actions on the orderItems (delete, add, update, etc.)
                case Choice.OrderItem:
                    switchOrderItem();
                    break;
                default:

                    break;
            }
            if (num == 0)//Exit the function without making another choice
                //Otherwise you will receive your choice
                return;
            else
            {
                Console.WriteLine(@"Shop Menu: 0-Exit
        1-Product
        2-Order 
        3-OrderItem ");
                if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
                num = num1;
                choice = (Choice)num;
            }
        }

    }
}
