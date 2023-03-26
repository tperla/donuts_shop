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
    /// Interaction logic for Ordered.xaml
    /// This is the class that represents the logic for the order confirmation window, where users can view their order number and a video.
    /// </summary>
    public partial class Ordered : Window
    {
        /// <summary>
        /// These are DependencyProperties that bind the window's "MyID" and "MyCustomerName" properties to the corresponding values.
        /// </summary>
        public int? MyID//It's an int variable that holds the order number that was created in the previous window
        {
            get { return (int)GetValue(MyIDProperty); }
            set { SetValue(MyIDProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyIDProperty =
            DependencyProperty.Register("MyID", typeof(int), typeof(Ordered), new PropertyMetadata(0));
        public string MyCustomerName//It's a string variable that holds the name of the customer that was created in the previous window.
        {
            get { return (string)GetValue(MyCustomerNameProperty); }
            set { SetValue(MyCustomerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCustomerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCustomerNameProperty =
            DependencyProperty.Register("MyCustomerName", typeof(string), typeof(Ordered), new PropertyMetadata(null));
        /// <summary>
        ///  The constructor for the class, it initializes the
        ///  component and sets the MyID and MyCustomerName properties to the passed id and name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Ordered(int? id, string name)
        {
            InitializeComponent();
            MyID = id;
            MyCustomerName = name;
            VideoControl.Play();//An object of the media element class that handles the playing of the video in the window.
        }
        /// <summary>
        ///  This event handler is called when the "back to catalog" button is clicked,
        ///  it stops the video, closes the current window and opens a new one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> telp = new();
            BO.Cart cart = new BO.Cart
            {
                CustomerName = MyCustomerName,
                CustomerAdress = "",
                CustomerEmail = "",
                Items = telp,
                TotalPrice = 0
            };
            openWindow newOpenWindow = new openWindow(cart);
            VideoControl.Stop();
            this.Close();
            newOpenWindow.ShowDialog();   
        }
        /// <summary>
        /// This event handler is called when the "main menu" button is clicked, 
        /// it stops the video, closes the current window and opens a new one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main newMain = new main();
            VideoControl.Stop();
            this.Close();
            newMain.ShowDialog();
        }
        /// <summary>
        ///  This event handler is called when the video ends, 
        ///  it sets the position of the video back to the beginning and plays it again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //made the video in loop
        private void VideoControl_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoControl.Position = TimeSpan.Zero;
            VideoControl.Play();
        }
    }
}
