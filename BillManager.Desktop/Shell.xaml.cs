﻿using MahApps.Metro.Controls;
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
using BillManager.Desktop.ViewModels;

namespace BillManager.Desktop
{
    public partial class Shell : MetroWindow
    {
        public Shell(ShellViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }       
    }
}