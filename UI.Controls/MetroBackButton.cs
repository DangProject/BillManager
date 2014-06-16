using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Controls
{  
    public class MetroBackButton : Button
    {
        static MetroBackButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroBackButton), new FrameworkPropertyMetadata(typeof(MetroBackButton)));
        }

        //public double ArrowHeight
        //{
        //    get { return (double)GetValue(ArrowHeightProperty); }
        //    set { SetValue(ArrowHeightProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ArrowHeight.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ArrowHeightProperty =
        //    DependencyProperty.Register("ArrowHeight", typeof(double), typeof(MetroBackButton), 
        //    new FrameworkPropertyMetadata(this ));
                
        public double ArrowHeight
        {
            get { return ActualHeight * .46; }
        }
        public double ArrowWidth
        {
            get { return ActualWidth * .6; }
        }
    }
}
