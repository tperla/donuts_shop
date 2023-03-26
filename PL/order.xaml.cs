using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static BO.Enums;
namespace PL
{
    /// <summary>
    /// Interaction logic for product.xaml
    /// </summary>
    public partial class order : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        public BO.Order? orderCurrent
        {
            get { return (BO.Order?)GetValue(orderCurrentProperty); }
            set { SetValue(orderCurrentProperty, value); }
        }

        public static readonly DependencyProperty orderCurrentProperty =
            DependencyProperty.Register("orderCurrent", typeof(BO.Order), typeof(order), new PropertyMetadata(null));
        public string MyState
        {
            get { return (string)GetValue(MyStateProperty); }
            set { SetValue(MyStateProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyStateProperty =
            DependencyProperty.Register("MyState", typeof(string), typeof(order), new PropertyMetadata("Show"));
        /// <summary>
        /// A constructive action that receives nothing and is for show mode
        /// </summary>
        public order(int ID, int x)
        {
            InitializeComponent();
            //Updates the button's name and title to show mode
            try
            {
                orderCurrent = bl?.Order?.GetOrder(ID);
            }
            catch (BO.BlMissingEntityException ex)
            {
                orderCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
        }
        /// <summary>
        /// Constructor action that receives a product ID and handles an update event
        /// </summary>
        /// <param name="ID"></param>
        public order(int ID)
        {
            InitializeComponent();
            BO.Order order = bl?.Order?.GetOrder(ID)!;
            MyState = "update";
            //Inserts the values ​​of the received product into the text boxes
            try
            {
                orderCurrent = order;
            }
            catch (BO.BlMissingEntityException ex)
            {
                orderCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            //this.DataContext=productCurrent;
        }
        /// <summary>
        ///When clicking on the update ship button, the order date will be updated
        ///to now and the status to ship - the button appears only when the status is ordered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateShip_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try {
                bl?.Order?.OrderShippingUpdate(orderCurrent!.ID);
                messageBoxResult = MessageBox.Show("Order shiped succefully 😁", "succefully🤩", MessageBoxButton.OK, MessageBoxImage.Information);
                orderCurrent = bl?.Order?.GetOrder(orderCurrent!.ID);
            }
            
             catch (BO.BlMissingEntityException ex)
            {
                orderCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
        }
        /// <summary>
        /// When clicking on the update delivery button, the order date will be updated to now and
        ///the delivery status - the button only appears when the status is shipped 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateDelivery_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl?.Order?.OrderDeliveryUpdate(orderCurrent!.ID);
                messageBoxResult = MessageBox.Show("Order delivered succefully 😁", "succefully 🤩", MessageBoxButton.OK, MessageBoxImage.Information);
                orderCurrent = bl?.Order?.GetOrder(orderCurrent!.ID);
            }
            catch (BO.BlIdDoNotExistException ex)
            {
                orderCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail 💥", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
        }
    }
}

