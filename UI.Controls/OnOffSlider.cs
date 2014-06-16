﻿using System;
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
    public class OnOffSlider : CheckBox
    {
        static OnOffSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OnOffSlider), new FrameworkPropertyMetadata(typeof(OnOffSlider)));
        }
    }
}