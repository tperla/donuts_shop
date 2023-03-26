using BlApi;
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


namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking
    /// </summary>
    /// <summary>
    ///The trakOrderWindow class is a partial class that represents a Window in a C# WPF application.
    ///   This window is for order tracking by entering an order code for an administrator and for a customer
    /// </summary>
    public partial class trakOrderWindow : Window
    {
        public trakOrderWindow()
        {
            InitializeComponent();
        }
        //bl is a nullable field of type BlApi.IBl, it is used to store the logic object.
        BlApi.IBl? bl = BlApi.Factory.Get();
        // OrderT is a nullable property of type BO.OrderTracking, it is used to get or set the OrderTracking object.
        public BO.OrderTracking? OrderT
        {
            get { return (BO.OrderTracking?)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }
        /// <summary>
        ///  OrderTrackingProperty is a static readonly DependencyProperty, it is used to register the OrderT property.
        /// <summary>
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderT", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        /// <summary>
        ///   The constructor takes an integer parameter id and it is used to initialize the OrderT property by calling the FollowOrder method on the bl object. 
        ///   If there is an exception of type BO.BlAllredyExistExeption, it shows the exception message in a message box. 
        ///   If there is an exception of type BO.BlMissingEntityException, it shows a message "The order was not found in the system" in a message box.
        /// <summary>
        public trakOrderWindow(int id)
        {
            InitializeComponent();
            try
            {
                OrderT = bl?.Order?.FollowOrder(id);//call to function in bl that return the status of order and traking order
            }
            catch (BO.BlAllredyExistExeption ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("The order was not found in the system", "ERROR😱");
            }
        }
    }
}

