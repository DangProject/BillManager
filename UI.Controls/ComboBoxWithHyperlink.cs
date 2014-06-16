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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Controls
{    
    public class ComboBoxWithHyperlink : ComboBox
    {
        static ComboBoxWithHyperlink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxWithHyperlink), new FrameworkPropertyMetadata(typeof(ComboBoxWithHyperlink)));
        }                

        public static readonly DependencyProperty ShowHyperlinkItemProperty =
            DependencyProperty.Register("ShowHyperlinkItem", typeof(bool), typeof(ComboBoxWithHyperlink), new PropertyMetadata(false));

        public static readonly DependencyProperty HyperlinkLabelProperty =
            DependencyProperty.Register("HyperlinkLabel", typeof(string), typeof(ComboBoxWithHyperlink), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty HyperlinkColorProperty =
            DependencyProperty.Register("HyperlinkColor", typeof(Brush), typeof(ComboBoxWithHyperlink), new PropertyMetadata(Brushes.SkyBlue));

        public static readonly DependencyProperty HyperlinkCommandProperty =
            DependencyProperty.Register("HyperlinkCommand", typeof(ICommand), typeof(ComboBoxWithHyperlink), new PropertyMetadata(default(ICommand)));
        
        public static readonly DependencyProperty HyperlinkCommandParameterProperty =
            DependencyProperty.Register("HyperlinkCommandParameter", typeof(object), typeof(ComboBoxWithHyperlink), new PropertyMetadata(default(object)));

        public bool ShowHyperlinkItem
        {
            get { return (bool)GetValue(ShowHyperlinkItemProperty); }
            set { SetValue(ShowHyperlinkItemProperty, value); }
        }
        
        public string HyperlinkLabel
        {
            get { return (string)GetValue(HyperlinkLabelProperty); }
            set { SetValue(HyperlinkLabelProperty, value); }
        }

        public Brush HyperlinkColor
        {
            get { return (Brush)GetValue(HyperlinkColorProperty); }
            set { SetValue(HyperlinkColorProperty, value); }
        }

        public ICommand HyperlinkCommand
        {
            get { return (ICommand)GetValue(HyperlinkCommandProperty); }
            set { SetValue(HyperlinkCommandProperty, value); }
        }

        public object HyperlinkCommandParameter
        {
            get { return (object)GetValue(HyperlinkCommandParameterProperty); }
            set { SetValue(HyperlinkCommandParameterProperty, value); }
        }
    }
}
