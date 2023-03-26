using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static BO.Enums;
using System.IO;
using System.Windows.Input;
using PL.cart;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for product.xaml
    /// </summary>
    public partial class product : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();//logic object
        //BO.Product? productCurrent = new BO.Product();
        public BO.Product? productCurrent
        {
            get { return (BO.Product?)GetValue(productCurrentProperty); }
            set { SetValue(productCurrentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productCurrentProperty =
            DependencyProperty.Register("productCurrent", typeof(BO.Product), typeof(product), new PropertyMetadata(null));
        public string MyStatus
        {
            get { return (string)GetValue(MyStatusProperty); }
            set { SetValue(MyStatusProperty, value); }
        }
        public bool heloPicture = false;
        // Using a DependencyProperty as the backing store for MyStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyStatusProperty =
            DependencyProperty.Register("MyStatus", typeof(string), typeof(product), new PropertyMetadata("Add"));
        /// <summary>
        /// A constructive action that receives nothing and is for add mode
        /// </summary>
        public product()
        {
            InitializeComponent();
            productCurrent = new BO.Product();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));  
        }
        /// <summary>
        /// Constructor action that receives a product ID and handles an update event
        /// </summary>
        /// <param name="ID"></param>
        public product(int ID)
        {
            MyStatus = "Update";
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));
            //Inserts the values ​​of the received product into the text boxes
            try
            {
                productCurrent = bl?.Product?.GetProduct(ID);
            }
            catch (BO.BlMissingEntityException ex)
            {
                productCurrent = null;
                MessageBox.Show(ex.Message, "Operation Fail💥", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlAllredyExistExeption ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
     
        /// <summary>
        /// Update or add product confirmation button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BlInvalidInputException"></exception>
        private void AddOrUpdateProduct_btn_Click(object sender, RoutedEventArgs e)
        {

            try//Checks the correctness of details
            {
                if (MyStatus != "Update")
                {
                    treatImage();//the fuction treat all things relevant to the image
                    bl?.Product?.AddProduct(productCurrent);
                }
                else
                {
                    if(heloPicture)
                    treatImage();//the fuction treat all things relevant to the image
                    bl?.Product?.UpdateProduct(productCurrent);
                }
                this.Close();
                MessageBox.Show($"Product "+MyStatus+"ed successfully🤩", "🍩", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString(),"ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(BO.BlAllredyExistExeption ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error in typing the items" , "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        /// <summary>
        ///When clicking the delete button, the product is deleted from the list of products in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Product?.DeleteProduct(productCurrent!.ID);
                this.Close();
                MessageBox.Show($"Product Deleted successfully🤩", "🍩", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch(BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        /// <summary>
        /// let the manager select an image from the browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    product_Image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    productCurrent!.picture = openFileDialog.FileName;
                    heloPicture = true;//update varible to go treatImage Function in the constructor
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("You  akready pick this picture");
            }
        }
        /// <summary>
        /// the function treats the things of the image
        /// </summary>
        private void treatImage()
        {
            if (productCurrent?.picture != null)
            {
                string imageName = productCurrent.picture.Substring(productCurrent.picture.LastIndexOf("\\"));
                if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\pics\" + imageName))
                    File.Copy(productCurrent.picture, Environment.CurrentDirectory[..^4] + @"\pictures\" + imageName);
                productCurrent.picture = @"\pictures\" + imageName;
            }
        }
        /// <summary>
        /// Allows when entering data to go down to the next text box by pressing the ENTER key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
