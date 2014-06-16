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
    public class EditButton : Button
    {
        static EditButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditButton), new FrameworkPropertyMetadata(typeof(EditButton)));
        }

        public static readonly DependencyProperty PencilHeightProperty =
            DependencyProperty.Register("PencilHeight", typeof(int), typeof(EditButton), new UIPropertyMetadata(12));
        public static readonly DependencyProperty PencilWidthProperty =
            DependencyProperty.Register("PencilWidth", typeof(int), typeof(EditButton), new UIPropertyMetadata(12));

        public int PencilHeight
        {
            get { return (int)GetValue(PencilHeightProperty); }
            set { SetValue(PencilHeightProperty, value); }
        }
        public int PencilWidth
        {
            get { return (int)GetValue(PencilWidthProperty); }
            set { SetValue(PencilWidthProperty, value); }
        }
    }
}
