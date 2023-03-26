using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.cart
{
    /// <summary>
    /// Interaction logic for cartWindowxaml.xaml
    ///  This is the class that represents the logic for the cart window.
    /// </summary>
    public partial class cartWindowxaml : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();//A reference to the BL API, it is used for updating the cart.
        //These are DependencyProperties that bind the window's "user" and "cart" properties to the corresponding objects.
        public BO.User user
        {
            get { return (BO.User)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }
        public static readonly DependencyProperty userProperty =
           DependencyProperty.Register("user", typeof(BO.User), typeof(cartWindowxaml), new PropertyMetadata(null));
        public BO.Cart? cart
        {
            get { return (BO.Cart?)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(cartWindowxaml), new PropertyMetadata(null));

        /// <summary>
        ///  The constructor for the class, it initializes the component and sets the cart property to the passed cart object.
        /// </summary>
        /// <param name="NewCart"></param>
        public cartWindowxaml(BO.Cart NewCart)
        {
            InitializeComponent();
            cart = NewCart;
        }
        /// <summary>
        /// This event handler is called when a double click is detected on the button, it closes the current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// This event handler is called when the "Order" button is clicked, it checks if the cart is empty and displays a message if it is,
        /// otherwise it opens a new window for login/signup or checkout based on whether the user has a name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goOrder_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cart!.Items!.Count() == 0)
            {
                MessageBox.Show("You cant order because your cart is empty","🛒");
                this.Close();
            }
            else
            {
                if (cart?.CustomerName == null || cart.CustomerName == "")
                {
                    CartLogOrSignW cartLogOrSignW = new CartLogOrSignW(cart!);
                    cartLogOrSignW.ShowDialog();
                    this.Close();
                }
                else
                {
                    InviteWindow inviteWindow = new InviteWindow(cart!);
                    this.Close();
                    inviteWindow.ShowDialog();  
                }
            }
        }
        //come back to katalog
        /// <summary>
        /// This event handler is called when the "back to catalog" button is clicked, it opens a new window and closes the current one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            openWindow o = new openWindow(cart!);
            this.Close();
            o.ShowDialog();
            
        }
        /// <summary>
        ///  This event handler is called when the "empty cart" button is clicked, it empties the cart, updates the UI, and refreshes the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void empty_Click(object sender, RoutedEventArgs e)
        {
            {
                List<BO.OrderItem>? temp = new();
                cart!.Items = temp;
                cart.TotalPrice = 0;
                ProductsItemsListBox.ItemsSource = cart!.Items;
                totalp_txt.Text = cart.TotalPrice.ToString();
                ProductsItemsListBox.Items.Refresh();
            }
        }

        /// <summary>
        /// This event handler is called when the "+" button is clicked, it increases the amount of the product in the cart and updates the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem p = ((BO.OrderItem)((Button)sender).DataContext);
            int id = p.ProductID;
            int amount = p!.Amount + 1;
            try
            {
                cart = bl!.Cart?.UpdateAmountOfProductInCart(cart, id, amount);
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("Product does not exist", "ERROR 😱", MessageBoxButton.OK!, MessageBoxImage.Exclamation);
            }
            catch (BlNotInStockException)
            {
                MessageBox.Show("The stock is finished 🤷", "🛒");
            }
                ProductsItemsListBox.ItemsSource = cart!.Items;
            totalp_txt.Text = cart.TotalPrice.ToString();
            ProductsItemsListBox.Items.Refresh();
        }
        /// <summary>
        /// This event handler is called when the "X" button is clicked, it deletes the item from the cart and updates the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem p = ((BO.OrderItem)((Button)sender).DataContext);
            int id = p.ProductID;
            try
            {
                cart = bl!.Cart?.UpdateAmountOfProductInCart(cart, id, 0);
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("Product does not exist", "ERROR 😱", MessageBoxButton.OK!, MessageBoxImage.Exclamation);
            }
            catch (BlNotInStockException)
            {
                MessageBox.Show("The stock is finished 🤷", "🛒");
            }
            ProductsItemsListBox.ItemsSource = cart!.Items;
            totalp_txt.Text = cart.TotalPrice.ToString();
            ProductsItemsListBox.Items.Refresh();
        }
        /// <summary>
        /// his event handler is called when the "-" button is clicked, it decreases the amount of the product in the cart and updates the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minus_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem p = ((BO.OrderItem)((Button)sender).DataContext);
            int id = p.ProductID;
            int amount = p!.Amount - 1;
            try
            {
                cart = bl!.Cart?.UpdateAmountOfProductInCart(cart, id, amount);
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("Product does not exist", "ERROR 😱", MessageBoxButton.OK!, MessageBoxImage.Exclamation);
            }
            catch (BlNotInStockException)
            {
                MessageBox.Show("The stock is finished 🤷", "🛒");
            }
            ProductsItemsListBox.ItemsSource = cart!.Items;
            totalp_txt.Text = cart.TotalPrice.ToString();
            ProductsItemsListBox.Items.Refresh();
        }
    }
}
