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
    public class NullToVisibilityConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty WhenNullProperty =
            DependencyProperty.Register("WhenNull", typeof(Visibility), typeof(NullToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));
        
        public static readonly DependencyProperty WhenNotNullProperty =
            DependencyProperty.Register("WhenNotNull", typeof(Visibility), typeof(NullToVisibilityConverter), new PropertyMetadata(Visibility.Visible));

        public Visibility WhenNull
        {
            get { return (Visibility)GetValue(WhenNullProperty); }
            set { SetValue(WhenNullProperty, value); }
        }

        public Visibility WhenNotNull
        {
            get { return (Visibility)GetValue(WhenNotNullProperty); }
            set { SetValue(WhenNotNullProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? WhenNull : WhenNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
