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
using System.Windows.Controls.Primitives;

namespace UI.Controls
{
    public class AudioOnOff : CheckBox
    {
        static AudioOnOff()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AudioOnOff), new FrameworkPropertyMetadata(typeof(AudioOnOff)));
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(AudioOnOff), new UIPropertyMetadata(25));
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(AudioOnOff), new UIPropertyMetadata(30));
        public static readonly DependencyProperty AudioOnForegroundProperty =
            DependencyProperty.Register("AudioOnForeground", typeof(Brush), typeof(AudioOnOff), new UIPropertyMetadata(Brushes.Blue));
        public static readonly DependencyProperty AudioOffForegroundProperty =
            DependencyProperty.Register("AudioOffForeground", typeof(Brush), typeof(AudioOnOff), new UIPropertyMetadata(Brushes.Gray));

        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }     
        }
        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public Brush AudioOnForeground
        {
            get { return (Brush)GetValue(AudioOnForegroundProperty); }
            set { SetValue(AudioOnForegroundProperty, value); }
        }
        public Brush AudioOffForeground
        {
            get { return (Brush)GetValue(AudioOffForegroundProperty); }
            set { SetValue(AudioOffForegroundProperty, value); }
        }
    }
}
