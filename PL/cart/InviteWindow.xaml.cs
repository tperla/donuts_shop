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
    /// Interaction logic for InviteWindow.xaml
    /// This is the class that represents the logic for the invite window, where users can select a shipping option and proceed to place their order.
    /// </summary>
    public partial class InviteWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//A reference to the BL API, it is used for placing the order.
        /// <summary>
        /// This is a DependencyProperty that binds the window's "MyCart" property to the corresponding cart object.
        /// </summary>
        public BO.Cart? MyCart
        {
            get { return (BO.Cart?)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }
        bool flag;//A boolean variable that is used to check if the delivery option has been previously selected.
        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(InviteWindow), new PropertyMetadata(null));

        public InviteWindow(BO.Cart Cart)
        {
            InitializeComponent();
            MyCart = Cart;
        }
        /// <summary>
        ///  This event handler is called when the "delivery" option is checked, 
        ///  it adds $20 to the total price of the cart and sets a flag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delivery_Checked(object sender, RoutedEventArgs e)
        {
            MyCart!.TotalPrice += 20;
            flag = true;
            totalprice_txt.Content = MyCart.TotalPrice;
            free.IsChecked = false;
        }
        /// <summary>
        /// This event handler is called when the "free" option is checked, it removes $20 from the total price
        /// of the cart if the delivery option has been previously selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void free_Checked(object sender, RoutedEventArgs e)
        {
            if (flag == true)
            {
                MyCart!.TotalPrice -= 20;
                totalprice_txt.Content = MyCart?.TotalPrice;
                delivery.IsChecked = false;
            }
        }
        /// <summary>
        /// This event handler is called when the "back to cart" button is clicked, it opens a new window and closes the current one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cartWindowxaml o = new cartWindowxaml(MyCart!);
            this.Close();
            o.ShowDialog();

        }
        /// <summary>
        ///  This event handler is called when the "order" button is clicked, it checks if a shipping option has been selected, 
        ///  and if so, it calls the BL API's OrderConfirmation method and opens a new window to display the order confirmation, 
        ///  otherwise it displays an error message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ordered_Click(object sender, RoutedEventArgs e)
        {
            if (delivery.IsChecked == true || free.IsChecked == true)
            {
                try
                {
                    int? id;
                    id = bl?.Cart?.OrderConfirmation(MyCart);
                    this.Close();
                    Ordered ordered = new Ordered(id, MyCart?.CustomerName!);
                    ordered.ShowDialog();
                    // MessageBox.Show("hello  "+cart?.CustomerName+ "Thank you for ordering from us, the order was successfully placed 😁❤ Your order number is:" + id+" "+ "We would love to see you again👍", "🍩");
                }
                catch (BO.BlInvalidInputException)
                {
                    MessageBox.Show("invalid input ->  you need to press again", "sorry 😕");
                }

                catch (BO.BlIdDoNotExistException)
                {
                    MessageBox.Show("Something went wrong", "ERROR 😱");
                }
            }
            else
            {
                MessageBox.Show("No shipping option selected", "ERROR 😱", MessageBoxButton.OK!, MessageBoxImage.Exclamation);
            }
        }
        /// <summary>
        /// When entering the details, you can go down to the next text box by pressing the enter key 
        /// </summary>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
