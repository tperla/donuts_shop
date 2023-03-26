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

namespace PL
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    public partial class main : Window
    {
        private bool _simulatorClick
        {
            get { return (bool)GetValue(_simulatorClickProperty); }
            set { SetValue(_simulatorClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _simulatorClickProperty =
            DependencyProperty.Register("_simulatorClick", typeof(bool), typeof(main), new PropertyMetadata(true));

        public main()
        {
            InitializeComponent();
            //start the video in screen
            MyVideo.Play();
        }
        /// <summary>
        /// Opens the window for menager with an empty basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void managerrOpen_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> telp = new();
            BO.Cart cart = new BO.Cart
            {
                CustomerName = "",
                CustomerAdress = "",
                CustomerEmail = "",
                Items = telp,
                TotalPrice = 0
            };
            loginWindow login = new loginWindow(cart);
            //stop the video in screen
            MyVideo.Pause();
            login.ShowDialog();
        }
        /// <summary>
        /// Opens the window for customers with an empty basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerOpen_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> telp = new();
            BO.Cart cart = new BO.Cart
            {
                CustomerName = "",
                CustomerAdress = "",
                CustomerEmail = "",
                Items = telp,
                TotalPrice = 0
            };
            openWindow open = new openWindow(cart);
            //stop the video in screen
            MyVideo.Pause();
            open.ShowDialog();
        }
        /// <summary>
        ///Makes the video loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            MyVideo.Position = TimeSpan.Zero;
            MyVideo.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _simulatorClick = false;
            //stop the video in screen
            MyVideo.Pause();
            new SimulatorWindow(() => _simulatorClick = !_simulatorClick).Show();
        }
    }
}
