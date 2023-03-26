using BlApi;
using BO;
using PL.cart;
using PL.opening;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Interaction logic for openWindow.xaml
    /// </summary>
    public partial class openWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        BO.Cart myCart;
        public int hourMain
        {
            get { return (int)GetValue(hourMainProperty); }
            set { SetValue(hourMainProperty, value); }
        }

        // Using a DependencyProperty as the backing store for hourMain.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty hourMainProperty =
            DependencyProperty.Register("hourMain", typeof(int), typeof(mainManager), new PropertyMetadata(0));
        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for myCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(openWindow), new PropertyMetadata(0));
        public string NameUser
        {
            get { return (string)GetValue(NameUserProperty); }
            set { SetValue(NameUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameUserProperty =
            DependencyProperty.Register("NameUser", typeof(string), typeof(openWindow), new PropertyMetadata(null));
        public string searchText
        {
            get { return (string)GetValue(searchTextProperty); }
            set { SetValue(searchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for searchText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty searchTextProperty =
            DependencyProperty.Register("searchText", typeof(string), typeof(openWindow), new PropertyMetadata(null));
        public bool flag
        {
            get { return (bool)GetValue(flagProperty); }
            set { SetValue(flagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for flag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty flagProperty =
            DependencyProperty.Register("flag", typeof(bool), typeof(openWindow), new PropertyMetadata(false));
        public IEnumerable<ProductItem?>? products
        {
            get { return (IEnumerable<ProductItem?>?)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }
       // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productsProperty =
            DependencyProperty.Register("products", typeof(IEnumerable<ProductItem?>), typeof(openWindow), new PropertyMetadata(null));
        /// <summary>
        ///Maximizes the client's basket and updates all the dispensary property
        /// </summary>
        /// <param name="cart"></param>
        public openWindow(Cart cart)
        {
            myCart = cart;
            hourMain = DateTime.Now.Hour;
            InitializeComponent();
            NameUser = cart.CustomerName!;
            Amount= bl!.Cart!.AmountOfItems(myCart);
            products = bl?.Product?.GetProducts();
        }
        /// <summary>
        /// Actions for all categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            BO.Enums.Category cat;
            string? category = (sender as TextBlock)?.Text;
            try
            {
                //show the right items:
                if (category != "Popular")
                {
                    Enum.TryParse(category, out cat);
                    ProductsItemsListBox.ItemsSource = bl?.Product?.GetListedProductsByCategory(cat);
                }
                else if (category == "Popular")
                    ProductsItemsListBox.ItemsSource = bl?.Product?.PopularItems();
                else
                    ProductsItemsListBox.ItemsSource = bl?.Product?.GetProducts();
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Error);

            }
}
        /// <summary>
        /// Opens the selected product in a big way and from there the customer can put it in the basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button)!.Tag.ToString());
            productKatalogWindow p = new productKatalogWindow(id, myCart);
            p.ShowDialog();
            try
            {
                Amount = bl!.Cart!.AmountOfItems(myCart);
            }
            catch(Exception ex )
            {
                MessageBox.Show("somthing wrong");
            }
        }
        /// <summary>
        ///When clicked, the basket window opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cart_btn_Click(object sender, RoutedEventArgs e)
        {
            cartWindowxaml cartw = new cartWindowxaml(myCart);
            cartw.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// Existing Customer -When clicking, a login window will pop up and the
        ///customer can enter his details and log in as a customer to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuLogIn_Click(object sender, RoutedEventArgs e)
        {
            loginWindow newlog = new loginWindow(myCart);
            
            newlog.ShowDialog();
            NameUser = myCart.CustomerName!;
        }
        /// <summary>
        /// When clicking, a login window will pop up and the
        ///customer can enter his details and log in as a customer to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSignIn_Click(object sender, RoutedEventArgs e)
        {
            loginWindow newlog = new loginWindow(2,myCart);
            NameUser = myCart.CustomerName!;
            newlog.ShowDialog();
        }
        /// <summary>
        ///When clicking, the customer will return to the catalog 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBack_Click(object sender, RoutedEventArgs e)
        {
            main m = new main();
            this.Close();   
            m.ShowDialog();
        }
        /// <summary>
        ///When clicking, the customer left the site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///When clicked, a window about the site will open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOdot_Click(object sender, RoutedEventArgs e)
        {
            Abaut a = new Abaut();
            a.ShowDialog();
        }
        /// <summary>
        /// When clicked, a content window will open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuContent_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow c=new ContactWindow();
            c.ShowDialog();
        }
        /// <summary>
        ///When clicked, an order tracking window will open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTrack_Click(object sender, RoutedEventArgs e)
        {
           trakingWindow traking = new trakingWindow();
            traking.ShowDialog();
        }
        /// <summary>
        ///When clicked, the selected product entered the shopping cart 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button)!.Tag.ToString());
            try
            {
                myCart = bl?.Cart?.AddProductToCart(id, myCart)!;
                Amount = bl!.Cart!.AmountOfItems(myCart);
            }

            catch (BO.BlDetailInCorrect)
            {
                MessageBox.Show("The stock is finished 🤷", "🛒");
            }
            catch (BO.BlNotInStockException)
            {
                MessageBox.Show("The stock is finished 🤷", "🛒");
            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("The product does not exist", "🛒");
            }
        }
        /// <summary>
        ///A button to search for items by writing the word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            string search = searchText;
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(ProductsItemsListBox.ItemsSource);
                view.Filter = item => ((ProductItem)item).Name!.Contains(search);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pleass-Try Again");
            }
        }
    }
}
