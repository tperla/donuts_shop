using BO;
namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// return the amount of the items in cart
        /// </summary>
        /// <param name="Cart"></param>
        /// <returns></returns>
        public int AmountOfItems(BO.Cart? Cart);
        /// <summary>
        /// add product to cart
        /// </summary>
        /// <returns></returns>
      public  Cart? AddProductToCart(int productId, Cart? Cart);
        /// <summary>
        /// update the amount of product in the cart
        /// </summary>
        /// <param name="cart"></param>
      public  Cart? UpdateAmountOfProductInCart(Cart? cart, int productId, int newAmount);
        /// <summary>
        /// make an order and return the id of the order
        /// </summary>
        /// <param name="cart"></param>
        /// <param name=""></param>
      public int OrderConfirmation(Cart? cart);
    }
}
    