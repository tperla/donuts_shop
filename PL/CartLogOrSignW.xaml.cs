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

//Window for the basket if entered without registering
namespace PL
{
    /// <summary>
    /// Interaction logic for CartLogOrSignW.xaml
    /// This is the class that represents the logic for the Cart login or signup window, 
    /// where users can select between logging in or signing up.
    /// </summary>
    public partial class CartLogOrSignW : Window
    {
        BO.Cart myC;// A variable that holds the cart object that was created in the previous window.
        /// <summary>
        /// The constructor for the class, it initializes the component and sets the myC variable to the passed Cart object
        /// </summary>
        /// <param name="c"></param>
        public CartLogOrSignW(BO.Cart c)
        {
            InitializeComponent();
            myC = c;
        }
        //If you click this button, you are connected to your device and log in
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginWindow l = new loginWindow(myC,true);
            this.Close();
            l.ShowDialog();
        }
        //If you click this button, you are connected to your device and sign in
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            loginWindow l = new loginWindow(3,myC);
            this.Close();
            l.ShowDialog();
        }
    }
}
