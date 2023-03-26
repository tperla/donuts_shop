using BlApi;
using System.Runtime.CompilerServices;
using OrderItem = BO.OrderItem;
namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal? dal = DalApi.Factory.Get();
        private static int index_orderItem = 1000000;//  running number for when we create a new orderItem
        /// <summary>
        /// func that add product to cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="Cart"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        /// <exception cref="BO.BlNotInStockException"></exception>
        public int AmountOfItems(BO.Cart? Cart)
        {
            int Amountp;
            IEnumerable<int> AmountOfItems = from item in Cart!.Items
                                             select item.Amount;
            Amountp = AmountOfItems.Sum();
            return Amountp;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Cart? AddProductToCart(int productId, BO.Cart? Cart)
        {
            try { 
            //check if the item exists in  dal 
            DO.Product productDo;
            try
            {
                productDo = dal!.product.Get(productId);
            }
            catch (DO.DalMissingIdException ex) //it doesn't exist in dal
            {
                throw new BO.BlMissingEntityException("Product does not exist", ex);
            }
            //check if the item exist in the cart 
            int orderItemIndex = Cart!.Items!.ToList().FindIndex(x => x?.ProductID == productId);    //yes= index, no=-1
            if (orderItemIndex != -1) //found in cart
            {
                BO.OrderItem? orderItem = Cart.Items!.ToList()[orderItemIndex];  //the product itself in place index
                if (productDo.InStock - orderItem.Amount >= 0) //there are alredy products in stock 
                {
                    //update the product in cart

                    Cart.Items!.ToList()[orderItemIndex].Price = productDo.Price;
                    int amount = Cart.Items!.ToList()[orderItemIndex].Amount;
                    amount++;
                    Cart.Items!.ToList()[orderItemIndex].Amount = amount;
                    Cart.Items!.ToList()[orderItemIndex].TotalPrice = amount * productDo.Price;
                    Cart.TotalPrice = amount * productDo.Price;
                }
                else //there aren't enough products in stock
                    throw new BO.BlNotInStockException(productDo.ID, productDo.Name);
            }
            else
            {
                //create new product:
                BO.OrderItem? newOrderItem = new BO.OrderItem()
                {
                    Price = productDo.Price,
                    Amount = 1,
                    ProductID = productDo.ID,
                    Name = productDo.Name ?? "",
                    TotalPrice = productDo.Price * 1,
                    picture = productDo.picture ?? @"\pictures\defult.PNG"
                };
                //update the Cart:
                ((List<BO.OrderItem?>)Cart.Items!).Add(newOrderItem);
                Cart.TotalPrice += productDo.Price /** helpAmount*/;
            }
            return Cart;
        }
             catch (DO.DalAlreadyExsistExeption ex) 
            {
                throw new BO.BlMissingEntityException("Cant ADD", ex);
            }
        }
        /// <summary>
        ///  func that make an order
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public int OrderConfirmation(BO.Cart? cart)
        {
            int orderID;
            BO.Product? BOProduct = new BO.Product();
            //check name
            if (cart?.CustomerName == null)
                throw new BO.BlInvalidInputException("Name of customer");
            //check  the email
            string e = cart?.CustomerEmail!;
            if (!e.Contains('@') || e.IndexOf('@') == 0 || e.IndexOf('@') == e.Length)
                throw new BO.BlInvalidInputException("Email of customer");
            //check adress
            if (cart?.CustomerAdress == null)
                throw new BO.BlInvalidInputException("Name of customer");
                    DO.Order OrderDo = new DO.Order()
                    {
                        CustomerName=cart.CustomerName,
                        CustomerAdress=cart.CustomerAdress,
                        CustomerEmail=cart.CustomerEmail,
                        OrderDate=DateTime.Now, 
                        DeliveryrDate= null,
                        ShipDate= null,  
                    };
                    try
                    {
                         orderID = dal!.order.Add(OrderDo);
                        
                        IEnumerable<int> orderItemID = from item in cart.Items
                                                       select dal.orderItem.Add(
                                                           new DO.OrderItem
                                                           {
                                                               OrderID = orderID,
                                                               Price = item.Price,
                                                               ProductID = item.ProductID,
                                                               Amount = item.Amount,
                                                               ID = item.ID,
                                                               Name = item.Name,
                                                               picture = item.picture ?? @"\pictures\defult.PNG"
                                                           });
               
                        IEnumerable<DO.Product> updateProduct = from item in cart.Items
                                                                select new DO.Product
                                                                {
                                                                    ID = item.ProductID,
                                                                    Name = item.Name,
                                                                    Price = item.Price,
                                                                    Category = dal.product.Get(item.ProductID).Category,
                                                                    InStock = dal.product.Get(item.ProductID).InStock - item.Amount,
                                                                    picture = dal.product.Get(item.ProductID).picture ?? @"\pictures\defult.PNG"
                                                                };
                        updateProduct?.ToList().ForEach(x => dal?.product.Update(x));
                                                   
                    }
                    catch(DO.DalIdDoNotExistException exeption)
                    {
                        throw new BO.BlIdDoNotExistException("Do not exsist!", exeption);
                    }
            catch (DO.DalAlreadyExsistExeption ex)
            {
                throw new BO.BlMissingEntityException("Cant ADD", ex);
            }
            return orderID;
        }
        /// <summary>
        /// func that update  amount of product by id in cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="newAmount"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlDetailInvalidException"></exception>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        /// <exception cref="BO.BlNotInStockException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Cart? UpdateAmountOfProductInCart(BO.Cart? cart, int productId, int newAmount)
        {
            try { 
            if (newAmount < 0)  //amount can't be negative:
                throw new BO.BlDetailInvalidException("New-Amount");
            //check if the item exists in the dal list:
            DO.Product? productDo;
            try
            {
                productDo = dal?.product.Get(productId);
            }
            catch (DO.DalMissingIdException ex) //it doesn't exist
            {
                throw new BO.BlMissingEntityException("Product does not exist", ex);
            }
            //check if the item exist in the cart already:
            int orderItemIndex = cart!.Items!.ToList().FindIndex(x => x?.ProductID == productId);    //yes= index, no=-1
            if (orderItemIndex != -1) //already in cart
            {
                OrderItem? orderItem = cart.Items!.ToList()[orderItemIndex];  //the product itself in place index
                if (productDo?.InStock >= newAmount) //there are enough products in stock due to the new amount
                {
                    //update:
                    cart.TotalPrice += newAmount * (int)productDo?.Price! - orderItem.Amount * orderItem.Price; //it works for newAmount>amount/newAmount<amount/newAmount=0
                                                                                                                //- we mult with different accesses to "price", so if a product will be deleted from dal/the price will be update,
                                                                                                                //and then will be added again, and then we will add it again to cart - the price will be updated well
                    cart.Items!.ToList()[orderItemIndex].Price = (int)productDo?.Price!;
                    cart.Items!.ToList()[orderItemIndex].Amount = newAmount;
                    cart.Items!.ToList()[orderItemIndex].TotalPrice = newAmount * (int)productDo?.Price!;
                    if (newAmount == 0) //remove from cart:
                        ((List<BO.OrderItem?>)cart.Items!).RemoveAt(orderItemIndex);
                }
                else //there aren't enough products in stock
                    throw new BO.BlNotInStockException((int)productDo?.ID!, productDo?.Name);
            }
            else  //isn't in cart:
            {
                if (productDo?.InStock >= newAmount) //there are enough products in stock due to the new amount
                {
                    //create new product:
                    BO.OrderItem? newOrderItem = new BO.OrderItem()
                    {
                        Price = (int)productDo?.Price!,
                        Amount = newAmount,
                        ProductID = (int)productDo?.ID!,
                        Name = productDo?.Name ?? "",
                        TotalPrice = (int)productDo?.Price! * newAmount,
                        picture = productDo?.picture ?? @"\pictures\defult.PNG"
                        //the ID of the order item is a runnung number that will be "given" in the next function:
                    };
                    //update:
                    ((List<BO.OrderItem?>)cart.Items!).Add(newOrderItem);

                    cart.TotalPrice += (int)productDo?.Price! * newAmount;
                }
                else //there aren't enough products in stock
                    throw new BO.BlNotInStockException((int)productDo?.ID!, productDo?.Name);
            }
        }
              catch (DO.DalAlreadyExsistExeption ex)
            {
                throw new BO.BlMissingEntityException("Cant ADD", ex);
            }
            return cart;
        }
    }
}
