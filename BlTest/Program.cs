using BO;
using static BO.Enums;
namespace BlTest
{
    internal class Program
    {
        //static IBl bl = new BlImplementation.Bl();
       static BlApi.IBl? bl = BlApi.Factory.Get();
        /* IEnumerable<ProductForList> ProductFL = bl.Product.GetListedProducts(); */
        private static BO.Cart CartBo = new BO.Cart()
        {
            CustomerName = "Daniel Levi",
            CustomerAdress = "Rashi 10",
            CustomerEmail = "Daniel123@gmail.com",
            Items = new List<BO.OrderItem>(),
            TotalPrice = 0,
        };
        #region switchToProducts
        static void switchProduct()
        {
            Console.WriteLine(@"Product: 0-Add
        1-Delete
        2-Get 
        3-GetAll
        4-GetProductsForList
        5-Update
        -1-Exit ");
            BO.Enums.ChoiceProduct choice;
            int num,num1,idProduct;
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (BO.Enums.ChoiceProduct)num;
            while (num >= 0)
            {
                int myId, stock;
                double price;
                switch (choice)
                {
                    case BO.Enums.ChoiceProduct.Add:
                        Console.WriteLine("Enter the product details for a new product");
                        BO.Product? product = new BO.Product();
                        Console.WriteLine("Enter the  name of the product");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Enter the  ID of the product");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        product.ID = myId;
                        Console.WriteLine("Enter the  price of the product");
                        if (double.TryParse(Console.ReadLine(), out price) == false) throw new BO.BlDetailInCorrect("incorrect price");
                        product.Price = price;
                        Console.WriteLine("Enter the  category of the product");
                        Console.WriteLine("Choose from the following options:");
                        Console.WriteLine("miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials");
                        BO.Enums.Category c;
                        string? input = Console.ReadLine();
                        bool check = BO.Enums.Category.TryParse(input, out c);
                        if (!check)
                            throw new Exception("you press a wrong Ctegory");
                        product.Category = c;
                        Console.WriteLine("Enter the  stock of the product");
                        if (int.TryParse(Console.ReadLine(), out stock) == false) throw new BO.BlDetailInCorrect("incorrect stock");
                        product.InStock = stock;
                        try
                        {
                            bl?.Product?.AddProduct(product);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceProduct.Delete:
                        Console.WriteLine("enter the id of the product that you want to delete");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BO.BlDetailInCorrect("incorrect id");
                         idProduct = myId;
                        try
                        {

                            bl?.Product?.DeleteProduct(idProduct);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceProduct.GetProducts:
                        IEnumerable<ProductForList> newProduct = new List<ProductForList>();
                        try
                        {
                            newProduct = bl?.Product?.GetListedProducts()!;//לא בטוח שצריך סימן קריאה..
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        foreach (var item in newProduct)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case BO.Enums.ChoiceProduct.GetProduct:
                        Console.WriteLine("enter id of product that you want to search");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BO.BlDetailInCorrect("incorrect id");
                         idProduct= myId;
                        try
                        {
                            Console.WriteLine(bl?.Product?.GetProduct(idProduct));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceProduct.GetProductForList:
                        Console.WriteLine("enter id of product: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        idProduct = myId;
                        try
                        {
                            Console.WriteLine(bl?.Product?.GetProduct(idProduct,CartBo));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceProduct.Update:
                        Console.WriteLine("Enter the product details that you want to update ");
                        BO.Product productUpdate = new BO.Product();
                        Console.WriteLine("Enter the  ID of the product");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        productUpdate.ID = myId;
                        //print the product before update
                        Console.WriteLine(bl?.Product?.GetProduct(productUpdate.ID));
                        //insert values to updateProduct
                        Console.WriteLine("Enter the  name of the product");
                        productUpdate.Name = Console.ReadLine();
                        Console.WriteLine("Enter the  price of the product");
                        if (double.TryParse(Console.ReadLine(), out price) == false) throw new BO.BlDetailInCorrect("incorrect price");
                        productUpdate.Price = price;
                        Console.WriteLine("Enter the  category of the product");
                        BO.Enums.Category cUpdate;
                        string? inputUpdate = Console.ReadLine();
                        bool checkUpdate = BO.Enums.Category.TryParse(inputUpdate, out cUpdate);
                        if (!checkUpdate)
                            throw new Exception("you press a wrong Ctegory");
                        productUpdate.Category = cUpdate;
                        Console.WriteLine("Enter the  stock of the product");
                        if (int.TryParse(Console.ReadLine(), out stock) == false) throw new BO.BlDetailInCorrect("incorrect stock");
                        productUpdate.InStock = stock;
                        try
                        {
                            bl?.Product?.UpdateProduct(productUpdate);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                   
                   
                    default:
                        Console.WriteLine("you press wrong number");
                        break;
                }
                Console.WriteLine(@"Product: 0-Add
        1-Delete
        2-Get 
        3-GetAll
        4-GetProductsForList
        5-Update
        -1-Exit ");
                if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
                num = num1;
                choice = (BO.Enums.ChoiceProduct)num;
            }
        }
        #endregion
        #region switchToOrders
        static void switchOrder()
        {
            Console.WriteLine(@"Order: 0-GetAllOrders
          1-GetOrder
          2-OrderDeliveryUpdate
          3-OrderOfTracking 
          4-ShippingUpdate 
          5-UpdateOrder 
         -1-Exit ");
            BO.Enums.ChoiceOrder choice;
            int num,num1, idOrder,idO;
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (BO.Enums.ChoiceOrder)num;
            while (num >= 0)
            {
                switch (choice)
                {
                    case BO.Enums.ChoiceOrder.GetAllOrders:
                        IEnumerable<OrderForList> order = new List<OrderForList>();
                        try
                        {
                           order=bl?.Order?.GetListedOrders()!;
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        foreach (OrderForList item in order)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case BO.Enums.ChoiceOrder.GetOrder:
                        Console.WriteLine("enter the id of the order ");
                        if (int.TryParse(Console.ReadLine(), out idO) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        idOrder  = idO;
                        try
                        {
                            Console.WriteLine(bl?.Order?.GetOrder(idOrder));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceOrder.OrderDeliveryUpdate:
                        Console.WriteLine("Enter the ID of order ");
                        if (int.TryParse(Console.ReadLine(), out idO) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        idOrder = idO;
                        try
                        {
                            Console.WriteLine(bl?.Order?.OrderDeliveryUpdate(idOrder));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceOrder.OrderOfTracking:
                        Console.WriteLine("enter id of order");
                        if (int.TryParse(Console.ReadLine(), out idO) == false) throw new BO.BlDetailInCorrect("incorrect id");
                       idOrder = idO;
                        try
                        {
                            Console.WriteLine(bl?.Order?.FollowOrder(idOrder));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceOrder.ShippingUpdate:
                        Console.WriteLine("Enter ID of the order");
                        if (int.TryParse(Console.ReadLine(), out idO) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        idOrder = idO;
                        try
                        {
                            Console.WriteLine(bl?.Order?.OrderShippingUpdate(idOrder));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case BO.Enums.ChoiceOrder.UpdateOrder:
                        Console.WriteLine("Enter the order details that you want to update ");
                        BO.Order orderUpdate = new BO.Order();
                        Console.WriteLine("Enter the ID of order ");
                        if (int.TryParse(Console.ReadLine(), out idO) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        orderUpdate.ID = idO;
                        //print the order before update
                        Console.WriteLine(bl?.Order?.GetOrder(orderUpdate.ID));
                        //insert values to updateOrder
                        Console.WriteLine("Enter your name ");
                        orderUpdate.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter your adress ");
                        orderUpdate.CustomerAdress = Console.ReadLine();
                        Console.WriteLine("Enter your email ");
                        orderUpdate.CustomerEmail = Console.ReadLine();
                        if (orderUpdate.ID < 1)
                            throw new BO.BlDetailInCorrect("ID is Incorrect");
                        if (orderUpdate.CustomerName == null)
                            throw new BO.BlDetailInCorrect("Name is Incorrect");
                        if (orderUpdate.CustomerAdress == null)
                            throw new BO.BlDetailInCorrect("Address is Incorrect");
                        if (orderUpdate.CustomerEmail == null || !orderUpdate.CustomerEmail.Contains('@'))
                            throw new BO.BlDetailInCorrect("Email is Incorrect");
                        try
                        {
                            //bonus
                            //bl.Order.UpdateOrder(orderUpdate);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Order: 0-GetAllOrders
          1-GetOrder
          2-OrderDeliveryUpdate
          3-OrderOfTracking 
          4-ShippingUpdate 
          5-UpdateOrder 
         -1-Exit ");
                if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
                num = num1;
                choice = (BO.Enums.ChoiceOrder)num;
            }
        }
        #endregion
        #region switchToCart
        static void switchCart()
        {
            Console.WriteLine(@"Cart:
             0-AddProductToCart
             1-MakeOrder
             2-UpdateAmountOfProduct
            -1-Exit ");
            ChoiceCart choice;
            int num,num1,ProductId,idP, amount;
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (ChoiceCart)num;
            while (num >= 0)
            {
                
                switch (choice)
                {
                    case ChoiceCart.AddToCart:
                        Console.WriteLine("enter id of product");
                        if (int.TryParse(Console.ReadLine(), out idP) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        ProductId = idP;
                        try
                        { bl?.Cart?.AddProductToCart(ProductId,CartBo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case ChoiceCart.MakeAnOrder:
                        try
                        {
                            bl?.Cart?.OrderConfirmation(CartBo);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case ChoiceCart.UpdateStockInCart:
                        Console.WriteLine("enter the id of product");
                        if (int.TryParse(Console.ReadLine(), out idP) == false) throw new BO.BlDetailInCorrect("incorrect id");
                        ProductId = idP;
                        Console.WriteLine("enter new amount of product");
                        if (int.TryParse(Console.ReadLine(), out amount) == false) throw new BO.BlDetailInCorrect("incorrect amount");
                        int amountP = amount;
                        if (amountP < 0)
                            throw new BO.BlDetailInCorrect("incorrect amount");
                        try
                        {
                            bl?.Cart?.UpdateAmountOfProductInCart(CartBo,ProductId,amountP);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Cart:
0-AddProductToCart
1-MakeOrder
2-UpdateAmountOfProduct
-1-Exit ");
                if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
                num = num1;
                choice = (ChoiceCart)num;
            }
   
        }
        #endregion
        static void Main(string[] args)
        {
            BO.Enums.Choice choice;
            int num,num1;
            Console.WriteLine("Shop Menu:");
            Console.WriteLine("0-Exit");
            Console.WriteLine("1-Product");
            Console.WriteLine("2-Order");
            Console.WriteLine("3-Cart ");
            if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
            num = num1;
            choice = (BO.Enums.Choice)num;
            //choose number 0-3
            while (num <= 3 && num >= 0)
            {
                switch (choice)
                {
                    case BO.Enums.Choice.Exit:
                        break;
                    //Call the function that doing actions on the products (add,delete,get...)
                    case BO.Enums.Choice.Product:
                        switchProduct();
                        break;
                    //Call the function that doing actions on the orders (add,delete,get...)
                    case BO.Enums.Choice.Order:
                        switchOrder();
                        break;
                    //Call the function that doing actions on the cart (add,delete,update...)
                    case BO.Enums.Choice.Cart:
                        switchCart();
                        break;
                    default:

                        break;
                }
                if (num == 0)//Exit the function without making another choice
                             //Otherwise you will receive your choice
                    return;
                else
                {
                    Console.WriteLine("Shop Menu:");
                    Console.WriteLine("0-Exit");
                    Console.WriteLine("1-Product");
                    Console.WriteLine("2-Order");
                    Console.WriteLine("3-Cart ");
                    if (int.TryParse(Console.ReadLine(), out num1) == false) throw new Exception("incorrect number");
                    num = num1;
                    choice = (BO.Enums.Choice)num;
                }
            }
        }
    }
}