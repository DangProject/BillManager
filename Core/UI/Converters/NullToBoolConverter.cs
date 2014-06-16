using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Core.UI
{
    public class NullToBoolConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty NullEqualsProperty =
            DependencyProperty.Register("NullEquals", typeof(bool), typeof(NullToBoolConverter), new PropertyMetadata(false));
        
        public bool NullEquals
        {
            get { return (bool)GetValue(NullEqualsProperty); }
            set { SetValue(NullEqualsProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? NullEquals : !NullEquals;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
