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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace BillManager.Desktop.Views
{
    public partial class ViewContainerWindow : MetroWindow
    {
        public ViewContainerWindow(IRegionManager regionManager)
        {
            this.SetValue(RegionManager.RegionManagerProperty, regionManager);
            InitializeComponent();
            this.Closed += (s, e) => regionManager.Regions.Remove(RegionNames.ViewContainerRegion);
                        
            //while (regionManager.Regions[RegionNames.ViewContainerRegion].Views.Count() > 0)
            //    regionManager.Regions[RegionNames.ViewContainerRegion].Remove(regionManager.Regions[RegionNames.ViewContainerRegion].Views.FirstOrDefault());            
        }        
    }
}
