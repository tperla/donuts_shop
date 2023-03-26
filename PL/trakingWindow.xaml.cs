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
    /// Interaction logic for trakingWindow.xaml
    /// <summary>
    /// <summary>
    ///    The trackingWindow class is a partial class that represents a Window in a C# WPF application.
   /// In this window, the customer or manager enters the order code and an order tracking window opens and shows the current order status
    /// <summary>
    public partial class trakingWindow : Window
    {
       //  bl is a nullable field of type BlApi.IBl, it is used to store the logic object.
        BlApi.IBl? bl = BlApi.Factory.Get();
       // idText is a string dependency property, it is used to get or set the idText value. which is connected in a binding to the text that the customer or manager enters in the order code
        public string idText
        {
            get { return (string)GetValue(idTextProperty); }
            set { SetValue(idTextProperty, value); }
        }
          //  idTextProperty is a static readonly DependencyProperty, it is used to register the idText property.
        public static readonly DependencyProperty idTextProperty =
            DependencyProperty.Register("idText", typeof(string), typeof(trakingWindow), new PropertyMetadata(null));
        /// <summary>
        ///The constructor for order tracking window.
        /// </summary>
        public trakingWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  The Ok_Click method is used to handle the click event of the OK button.
        //it tries to parse the string value of the idText property to an integer,
        ///    it creates a new instance of the trakOrderWindow class and pass the id of the order as a parameter to the constructor.
        ///    After that, it calls the ShowDialog method on the trackorder instance to show the window as a dialog box.
        ///    If there is an exception of type BO.BlAllredyExistExeption, it shows the exception message in a message box.
        ///    If there is an exception of type BO.BlMissingEntityException, it shows a message "The order was not found in the system" in a message box.
        ///    Lastly, it closes the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(idText);
                //Opens the tracking window for the entered order if it exists in the order database
                trakOrderWindow trackorder = new trakOrderWindow(bl!.Order!.FollowOrder(id)!.Id);
                trackorder.ShowDialog();
            }
            catch (BO.BlAllredyExistExeption ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("The order was not found in the system", "ERROR😱");
            }
            Close();
        }
    }
}
