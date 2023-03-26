using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static BO.Enums;
namespace PL
{
    /// <summary>
    /// Interaction logic for productForList.xaml
    /// </summary>
    public partial class productForList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        /// <summary>
        /// constructor that build a window for the manneger of to take care of the products list
        /// </summary>
        /// <param name="myBl"></param>
        public productForList(BlApi.IBl? myBl)
        {
            bl = myBl;
            InitializeComponent();
            try
            {
                productListView.ItemsSource = bl?.Product?.GetListedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
            categorySelector_cmb.ItemsSource = Enum.GetValues(typeof(Category));
            categorySelector_cmb.SelectedValue = Category.None;
        }
        /// <summary>
        /// event that show the list of the same category that had been chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categorySelector_cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Handles the condition of none
            if (categorySelector_cmb.SelectedIndex == 7) 
            {
                try
                {
                    productListView.ItemsSource = bl?.Product?.GetListedProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong");
                }
                categorySelector_cmb.SelectedItem = null;
            }
            //Filters the list by selected category
            if (categorySelector_cmb.SelectedItem != null)
            {
                Category category = (BO.Enums.Category)categorySelector_cmb.SelectedItem;
                try
                {
                    productListView.ItemsSource = bl?.Product?.GetListedProductsByCategory(category);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }
        /// <summary>
        /// button that open the addition product window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            product addProductWindow = new product();
            addProductWindow.ShowDialog();
            //Automatically updates after clicking OK when adding a product
            try
            {
                productListView.ItemsSource = bl?.Product?.GetListedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }
        /// <summary>
        /// Event of double clicking on a product - opens a new product update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Sending a product ID number to the update window so that it is written there
            int id = ((BO.ProductForList)productListView.SelectedItem).ID;
            product updateProductWindow = new product(id);
            //The id will be in a state of unchangeable lack
            updateProductWindow.ShowDialog();
            //Automatically updates after clicking OK when adding a product
            try
            {
                productListView.ItemsSource = bl?.Product?.GetListedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void productListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
