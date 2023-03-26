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
    /// Interaction logic for prosucrKatalogWindow.xaml
    /// </summary>
    public partial class productKatalogWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        public BO.ProductItem? productCurrent
        {
            get { return (BO.ProductItem?)GetValue(productCurrentProperty); }
            set { SetValue(productCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productCurrentProperty =
            DependencyProperty.Register("productCurrent", typeof(BO.ProductItem), typeof(productKatalogWindow), new PropertyMetadata(null));
        BO.Cart myCart;
        public productKatalogWindow(int ID,BO.Cart cart)
        {
            myCart = cart;
            InitializeComponent();
            try
            {
                productCurrent = bl?.Product?.GetProduct(ID,cart);
            }
            catch (BO.BlMissingEntityException ex)
            {
                productCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlAllredyExistExeption ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        /// <summary>
        ///When clicking the add button, the product is added to the customer's shopping cart and the quantity is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button)!.Tag.ToString());
            try
            {
                myCart = bl?.Cart?.AddProductToCart(id, myCart)!;
            }
            catch(BO.BlMissingEntityException)
            {
                MessageBox.Show("The product is no longer found ‍", "sorry🤷‍");
            }
            catch(BO.BlNotInStockException)
            {
                MessageBox.Show("The product is out of stock‍", "sorry🤷‍");
            }
            this.Close();
        }
    }
}
