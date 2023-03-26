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
    /// Interaction logic for mainManager.xaml
    /// </summary>
    public partial class mainManager : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public int currentHour
        {
            get { return (int)GetValue(currentHourProperty); }
            set { SetValue(currentHourProperty, value); }
        }
        // Using a DependencyProperty as the backing store for currentHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentHourProperty =
            DependencyProperty.Register("currentHour", typeof(int), typeof(mainManager), new PropertyMetadata(0));
        public BO.User user
        {
            get { return (BO.User)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }
        public static readonly DependencyProperty userProperty =
           DependencyProperty.Register("user", typeof(BO.User), typeof(mainManager), new PropertyMetadata(null));
        public mainManager(BO.User newUser)
        {
            InitializeComponent();
            user = newUser;
            currentHour = DateTime.Now.Hour;
        }
        private void Button_Click(object sender, RoutedEventArgs e) => new productForList(bl).ShowDialog();
        private void Button_Click_1(object sender, RoutedEventArgs e) => new orderForList(bl).ShowDialog();
        private void Button_Click_2(object sender, RoutedEventArgs e) => new trakingWindow().ShowDialog();
        /// <summary>
        ///When clicked, a new administrator will be added to the site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewManager_btn_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> help = new();
            BO.Cart cart = new BO.Cart
            {
                CustomerName = "",
                CustomerAdress = "",
                CustomerEmail = "",
                Items = help,
                TotalPrice = 0
            };
            loginWindow loginWindow = new loginWindow(1, cart);
            loginWindow.ShowDialog();   
        }
        /// <summary>
        ///When clicked, it will return to the opening page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void return_btn_Click(object sender, RoutedEventArgs e)
        {
            main m = new main();
           Close();
            m.ShowDialog();   
        }
    }
}
