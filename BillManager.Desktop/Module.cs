using BillManager.Desktop.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using BillManager.Desktop.Interfaces;

namespace BillManager.Desktop
{
    public class Module : IModule
    {
        IRegionManager regionManager;
        IUnityContainer container;
        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<object, MainShell>(typeof(MainShell).Name);
            container.RegisterType<object, BillContentView>(typeof(BillContentView).Name);
            container.RegisterType<object, BillSummaryView>(typeof(BillSummaryView).Name);
            container.RegisterType<object, CreateBillView>(typeof(CreateBillView).Name);
            container.RegisterType<object, CreateCategoryView>(typeof(CreateCategoryView).Name);
            container.RegisterType<object, CreateWebsiteView>(typeof(CreateWebsiteView).Name);
                        
            //container.RegisterInstance<IPresentationController>(container.Resolve<PresentationController>());
            container.RegisterInstance<IStartupManager>(container.Resolve<StartupManager>());

            regionManager.RegisterViewWithRegion(RegionNames.ShellRegion, typeof(LogonView));            
        }
    }
}
