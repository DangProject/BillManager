﻿using System;
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

namespace BillManager.Desktop.Views
{
    public partial class CreateBillView : UserControl
    {
        public CreateBillView(CreateBillViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
            //this.Loaded += (s, e) =>
            //{
            //    this.DataContext = viewModel;
            //};
        }
    }
}
