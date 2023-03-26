using BO;
using PL.cart;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// constructor: Initializes the login window with a cart object and flag variable. It also creates a new user object.
    /// </summary>
    public partial class loginWindow : Window
    {
        //property: holds the user's information.
        public User MyUser
        {
            get { return (User)GetValue(MyUserProperty); }
            set { SetValue(MyUserProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyUser.  This enables animation, styling, binding, etc...
        // dependency property: Backing store for MyUser property.
        public static readonly DependencyProperty MyUserProperty =
            DependencyProperty.Register("MyUser", typeof(User), typeof(loginWindow), new PropertyMetadata(null));
        //property: holds a value passed in when the sign in constructor is called.
        public int check
        {
            get { return (int)GetValue(checkProperty); }
            set { SetValue(checkProperty, value); }
        }
        // Using a DependencyProperty as the backing store for check.  This enables animation, styling, binding, etc...
        //property: dependency property: Backing store for check property.
        public static readonly DependencyProperty checkProperty =
            DependencyProperty.Register("check", typeof(int), typeof(loginWindow), new PropertyMetadata(0));
        BlApi.IBl? bl = BlApi.Factory.Get();//variable: holds a reference to the business logic object.
        BO.Cart myCart;//variable: holds a reference to the cart object.
        bool flagCart;//variable: holds a boolean value that determines whether the cart window will open or not.
        //log in
        public loginWindow( Cart c,bool flag=false)
        {
            myCart = c;
            flagCart = flag;
            MyUser = new User();
            InitializeComponent();
        }
        //sign in
        /// <summary>
        ///  constructor: Initializes the login window with a cart object and an int variable. It also creates a new user object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="c"></param>
        public loginWindow(int x ,Cart c)
        {
            InitializeComponent();
            myCart = c;
            MyUser = new User();
            check = x;
        }
        //log in
        /// <summary>
        /// function: Attempts to retrieve user's information from the business logic, 
        /// if successful it opens a new window based on user's status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string passwordCheck;
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            try//check if user almost found in list of user
            {
                passwordCheck = MyUser.Password!;
              User userHelp=bl?.User.GetByUser(MyUser.UserName!)!;
                if(userHelp.Password!=passwordCheck)
                {
                    MessageBox.Show("Password is not good", "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MyUser = userHelp;
            }
            catch(BO.BlDetailInvalidException)
            {
                MessageBox.Show("Username is empty", "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            catch(BO.BlMissingEntityException)
            {
                MessageBox.Show("sorry you are not found", "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //check if maneger
            if (MyUser.state == BO.Enums.State.menager)
            {

                Close();
                new mainManager(MyUser).ShowDialog();
            }
            else//you are customer
            {
                if (!flagCart)//You arrived from a catalog window
                {
                    myCart.CustomerName= MyUser.UserName;
                    openWindow o = new openWindow(myCart);
                    Close();
                    o.ShowDialog();
                }
                else//You arrived from a basket window
                {
                    myCart.CustomerName = MyUser.UserName;
                    cartWindowxaml o = new cartWindowxaml(myCart);
                    Close();
                    o.ShowDialog();
                }
            }
        }
        //  sign in
        /// <summary>
        /// function: checks if the username is already taken, 
        /// if not it adds the user to the system and opens a new window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsign_Click(object sender, RoutedEventArgs e)
       {
            try
            {
                MyUser = bl?.User.GetByUser(MyUser.UserName!)!;
                if (MyUser != null)
                {
                    MessageBox.Show("sorry this user name is alredy taken 🤦‍", "ERROR 😱", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (BO.BlDetailInvalidException)
            {
                MessageBox.Show("Username is empty", "ERROR😱", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            catch (BO.BlMissingEntityException)
            {
                if (check == 1)//you are menager
                    MyUser.state = Enums.State.menager;
                else//customer
                    MyUser.state = Enums.State.customer;
                try//add the user to list of user
                {
                    bl?.User.Add(MyUser);
                }
                catch(BO.BlDetailInvalidException)
                {
                    MessageBox.Show("ERROR 😱");
                }
                catch(BO.BlInvalidInputException)
                {
                    MessageBox.Show("ERROR 😱");
                }
                //if the customer enter menager details
                if (MyUser.state == BO.Enums.State.menager)
                    new mainManager(MyUser).ShowDialog();
                else
                {
                    if (check != 3)
                    {
                        myCart.CustomerName = MyUser.UserName;
                        openWindow o = new openWindow(myCart);
                        this.Close();
                        o.ShowDialog();
                    }
                    else//ypu arraived from cart
                    {
                        myCart.CustomerName = MyUser.UserName;
                        cartWindowxaml o = new cartWindowxaml(myCart);
                        this.Close();
                        o.ShowDialog();
                    }
                }       
            }
        }
        /// <summary>
        /// When entering the details, you can go down to the next text box by pressing the enter key 
        /// </summary>
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
