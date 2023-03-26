using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL
{
    /// <summary>
    ///  Converts the value of the button text "Update Product" to be disabled.
    /// </summary>
    class ConvertIDIsEnabled : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Update Product")
                return false;
            else
                return true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts the given image path to a BitmapImage. If an exception is thrown, it returns the default image instead.
    /// </summary>
    class ConvertTextToAdd : IMultiValueConverter //As long as one of the product details is empty, it will not be possible to update or add a new product
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            while (value[0].ToString() == "0" || value[0].ToString() == "" || value[1].ToString() == "" || value[1].ToString() == "0" || value[2].ToString() == "" || value[2].ToString() == "0" || value[3].ToString() == "" || value[4] == null)
            {
                return false;
            }
            return true;

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertImagePathToBitMap :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string ImagePathToBitMap = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string ImageFullName = currentDir + ImagePathToBitMap;
                BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFullName));
                return bitmapImage;
            }
            catch(Exception ex)
            {
                string ImagePathToBitMap = @"\pictures\defult.PNG";
                string currentDir = Environment.CurrentDirectory[..^4];
                string ImageFullName = currentDir + ImagePathToBitMap;
                BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFullName));
                return bitmapImage;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts the visibility of the element based on the shipping status. If the status is "Shipped", 
    /// it sets the visibility to Visible. Otherwise, it sets it to Hidden.
    /// </summary>
    class ConvertDeliveryVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Shipped")
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Checks the status of an order and accordingly it is appropriate whether to give the option of updating the shipping date or not
    /// </summary>
    class ConvertShipVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Ordered")
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts the value of the button text "Add New Product" to be enabled and "Update Product" to be disabled.
    /// </summary>
    class convertToIsEnabledByStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Add")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts the value of the button text based on the status. If the status is "Add", 
    /// it sets the text to "Add New Product", otherwise it sets it to "Update Product".
    /// </summary>
    class convertByStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Add")
            {
                return "Add New Product";
            }
            else
            {
                return "Update Product";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertTheContentByState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Show")
            {
                return "The details of an order";
            }
            else
            {
                return "Update order";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts the value of the element based on the state. If the state is "Add", 
    /// it sets the text to "Add New Product", otherwise it sets it to "Update Product".
    /// </summary>
    class ConvertTextToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "0") 
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    ///  Converts a value (a string representation of an int) to a Visibility. If the value is "0", 
    ///  returns Visibility.Visible, otherwise returns Visibility.Hidden.
    /// </summary>
    class ConvertTextToVisibility1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "0")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts a value (a string) to a Visibility. If the value is null or empty, 
    /// returns Visibility.Visible, otherwise returns Visibility.Hidden.
    /// </summary>
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string) ? Visibility.Visible : Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts a value to Visibility type, returns Visibility.Hidden if value matches "Add New Product" otherwise Visibility.Visible.
    /// </summary>
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Add New Product") 
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts a int value to Brush type, returns LightGray if value is 0 otherwise White.
    /// </summary>
    class ConvertBooleanToColors : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue==0)
            {
                return Brushes.LightGray;
            }
            else
            {
                return Brushes.White;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    ///  Converts a int value to Visibility type, returns Visibility.Hidden if value is 0 otherwise Visibility.Visible.
    /// </summary>
    class ConvertBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue == 0)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts int to Visibility, 0 = Hidden, else = Visible.
    /// </summary>
    class convertLogIn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue==0)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts int to Visibility, non-0 = Hidden, else = Visible.
    /// </summary>
    class convertSignIn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue != 0)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Converts int to a string representing time of day, 6-12 = "Good Morning", 
    /// 12-18 = "good afternoon", 18-22 = "good evening", else = "Good night".
    /// </summary>
    class convertTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue >= 6 && intValue < 12)
            {
                // morning
                return "Good Morning";
            }
            else if (intValue >= 12 && intValue < 18)
            {
                // in afternoon
                return "good afternoon";
            }
            else if (intValue >= 18 && intValue < 22)
            {
                //evening
                return "good evening";
            }
            else
            {
                // in night
                return "Good night";

            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

