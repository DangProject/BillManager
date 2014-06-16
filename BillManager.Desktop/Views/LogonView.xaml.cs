using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BillManager.Desktop.Views
{
    public partial class LogonView : UserControl
    {
        public LogonView(LogonViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
            //this.Loaded += (s, e) => Keyboard.Focus(userNameBox);
        }       

        //private void RadWatermarkTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    PasswordBox.Visibility = Visibility.Visible;            
        //    FocusManager.SetFocusedElement(this, PasswordBox);
        //}

        //private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("PasswordBox_LostFocus");
        //    PasswordBox.Visibility = Visibility.Hidden;
        //}

        //private void RadWatermarkTextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("RadWatermarkTextBox_GotFocus");
        //    PasswordBox.Visibility = Visibility.Hidden;
        //}
    }
}
