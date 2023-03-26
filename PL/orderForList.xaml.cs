using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static BO.Enums;
namespace PL
{
    /// <summary>
    /// Interaction logic for orderForList.xaml
    /// </summary>
    public partial class orderForList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        public orderForList(BlApi.IBl? myBl)
        {
            bl = myBl;
            InitializeComponent();
            try
            {
                orderForListDataGrid.ItemsSource = bl?.Order?.GetListedOrders();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
            statusSelector_cmb.ItemsSource = Enum.GetValues(typeof(OrderStatus)); 
            statusSelector_cmb.SelectedValue = OrderStatus.None;
        }
        private void statusSelector_cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Handles the condition of none
            if (statusSelector_cmb.SelectedIndex == 3)
            {
                try
                {
                    orderForListDataGrid.ItemsSource = bl?.Order?.GetListedOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong");
                }
                statusSelector_cmb.SelectedItem = null;
            }
            //Filters the list by selected status
            if (statusSelector_cmb.SelectedItem != null)
            {
                OrderStatus status = (BO.Enums.OrderStatus)statusSelector_cmb.SelectedItem;
                try
                {
                    orderForListDataGrid.ItemsSource = bl?.Order?.GetListedOrdersByStatus(status);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }
        /// <summary>
        /// A window opens to see the order details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// A window opens to update the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderForList)orderForListDataGrid.SelectedItem).ID; 
            order updateOrderWindow = new order(id);
            //The id will be in a state of unchangeable lack
            updateOrderWindow.ShowDialog();
            //Automatically updates after clicking OK when adding an order
            orderForListDataGrid.ItemsSource = bl?.Order?.GetListedOrders();
        }
        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)orderForListDataGrid.SelectedItem).ID;
            order updateOrderWindow = new order(id, 2);
            //The id will be in a state of unchangeable lack
            updateOrderWindow.ShowDialog();
            //Automatically updates after clicking OK when adding an order
            orderForListDataGrid.ItemsSource = bl?.Order?.GetListedOrders();
        }

        private void orderForListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
