using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;
using static Dal.DataSource;
using static DO.Enums;
namespace Dal;
internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random rnd = new Random();
    internal static class config
    {  
        //run number to order
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int _nextOrderNumber { get => s_nextOrderNumber++; }
        //run number to orderItem
        internal const int s_startOrderItem = 1000;
        private static int s_nextOrderItem = s_startOrderItem;
        internal static int _nextOrderItem { get => s_nextOrderItem++; }
    }
    internal static List<Order?> _orderList { get; } = new List<Order?>();
    internal static List<OrderItem?> _orderItemList { get; } = new List<OrderItem?>();
    internal static List<Product?> _productList { get; } = new List<Product?>();
    internal static List<Users?> _usersList { get; } = new List<Users?>();
    private static void s_Initialize()
    {
        //Console.WriteLine("In Init");
        InitCreatProductToList();
        InitCreatOrderToList();
        InitCreatOrderItemToList();
        InitCreatUserNameToList();
        //XMLTools.SaveListToXMLSerializer(_productList, "Product");
        //XMLTools.SaveListToXMLSerializer(_orderList, "Order");
        //XMLTools.SaveListToXMLSerializer(_orderItemList, "OrderItem");
        //XMLTools.SaveListToXMLSerializer(_usersList, "User");
    }
    #region fill the products

    private static string[,] nameProduct = new string[7, 4] {/*miniDnuts*/ { "OreoCase","NutellaCase", "LotusCase","MiniCase" }
        /*belgianWaffles*/ ,{"OreoWaffel","NutellaWaffel", "LotusWaffel","Colorful"},
        /*general*/ {"Brauniz","StarDnuts", "HeartDonuts","BigBuston"},
        /*bigDonuts*/ {"BigCase","BigNutella", "BigLotus","BigOreo"},
       /*specials*/ {"America","Purim", "LoveCase","Chanuka" },
       /*cupcakes*/{"ColorfulCup","Choclate","Fistuc","Orange"} ,
       /*desserts*/{"Oreo25","Malabi", "Lotus25","Mix" }  };
    //array to price range
    private static int[] priceStart = new int[7] { 12, 10, 15, 20, 25, 30, 22 };
    private static int[] priceTo = new int[7] { 35, 40, 50, 60, 57, 45, 70 };
    //array to stock
    private static int[] inStock = new int[7] { 50, 90, 45, 30, 20, 17, 80 };
    //fill 10 product in array
    private static void InitCreatProductToList()
    {
        int helpId = 0;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Product newProduct = new Product
                {
                    ID = helpId+ 100000,
                    Name = nameProduct[i, j],
                    Price = rnd.Next(priceStart[i], priceTo[i]),
                    InStock =rnd.Next(inStock[i]),
                    Category = (Category)i,
                    picture = @"\pictures\" + nameProduct[i, j] + ".PNG"
                };
                _productList.Add(newProduct);
                helpId++;
            }
        }
    }
    #endregion
    #region fill the orders
    //array to name f customer
    private static string[] customerName = { "Shira", "Noa", "Eli", "Daniel", "Avi" };
    //array to adress of customer
    private static string[] customerAdress = { "EvenGvirol", "Daniel", "Rashi", "ChoniHmeagel", "Narkis" };
    //fill 20 order to array
    private static void InitCreatOrderToList()
    {
        for (int i = 0; i < 20; i++)
        {
            int order = rnd.Next(5);
            int days = rnd.Next(100, 600);
            int hours = rnd.Next(2, 20);
            Order newOrder = new Order()
            {
                ID = config._nextOrderNumber,
                CustomerName = customerName[order],
                CustomerAdress = customerAdress[rnd.Next(5)],
                CustomerEmail = customerName[order] + "@gmail.com",
                OrderDate = DateTime.Now - new TimeSpan(0, hours, 0, 0),
                ShipDate = null,
                DeliveryrDate = null
            };
            //date and time
           
            if (i < 0.8 * 20)
            {
                hours=rnd.Next(1,5);
                TimeSpan shipTime = new TimeSpan(0, hours, 0, 0);
                newOrder.ShipDate = newOrder.OrderDate + shipTime;
            }

            if (i < 0.6 * 20*0.8)
            {
                newOrder.OrderDate = DateTime.Now - new TimeSpan(days, 0, 0, 0);
                hours = rnd.Next(0,2);
                TimeSpan shipTime = new TimeSpan(0, hours, 0, 0);
                newOrder.ShipDate = newOrder.OrderDate + shipTime;
                hours = rnd.Next(0,5);
                TimeSpan deliveryTime = new TimeSpan(0, hours, 0, 0);
                newOrder.DeliveryrDate = newOrder.ShipDate + deliveryTime;
            }
            _orderList.Add(newOrder);
        }
    }
    #endregion
    #region fill the orderItems
    //fill  orderItem to array
    private static void InitCreatOrderItemToList()
    {
        int IDHelp = rnd.Next(10);  //for the product ID
        for (int i = 0; i < 20; i++)    //20 orders*2 items=40 itmes
        {
            //item 1:
            _orderItemList.Add(
                new OrderItem
                {
                    ID = (i / 2),
                    OrderID = config._nextOrderItem,
                    ProductID = 100000 + IDHelp, //some randomaly product
                    Price = _productList.Find(x => (x?.ID == 100000 + IDHelp))?.Price ?? throw new DO.DalMissingIdException("Product"),    //price=the price of the product
                    Amount = rnd.Next(10) + 1,
                    picture = _productList.Find(x => (x?.ID == 100000 + IDHelp))?.picture ?? throw new DO.DalMissingIdException(" Product")

                });
            //ditto for item 2:
            int IDHelp2 = rnd.Next(18);
            while (IDHelp2 == IDHelp)
            {
                IDHelp2 = rnd.Next(18);
            }
            _orderItemList.Add(
               new OrderItem
               {
                   ID = (i / 2),
                   OrderID = config._nextOrderItem,
                   ProductID = 100000 + IDHelp, //some randomaly product
                   Price = _productList.Find(x => (x?.ID == 100000 + IDHelp))?.Price ?? throw new DO.DalMissingIdException("Product"),    //price=the price of the product
                   Amount = rnd.Next(10) + 1,
                   picture = _productList.Find(x => (x?.ID == 100000 + IDHelp))?.picture ?? throw new DO.DalMissingIdException(" Product")
               });
        }
    }
        #endregion
    #region fill the users
        private static void InitCreatUserNameToList()
        {
            string[] name = new string[] { "Shira", "Noa", "Eli", "Rachel" };
            string[] nameMenager = new string[] { "Debbi", "Tali" };
            string[] passwordd = new string[] { " 123456", "12346", " 222222", "547666" };
            for (int i = 0; i <2; i++)
            {
                Users newmenager = new Users();

                newmenager.password = "1234";
                newmenager.UserName = nameMenager[i];
                newmenager.state = State.menager;

                _usersList.Add(newmenager);
            }
            for (int i = 0; i < 3; i++)
            {
                Users newuser = new Users()
                {
                    UserName = name[i],
                    password = passwordd[i],
                    state = State.customer
                };
                _usersList.Add(newuser);
            }

        }
    }

#endregion







