using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using BillManager.Desktop.ViewModels;
using System.Threading;

namespace BillManager.Desktop
{    
    public class Bootstrapper : UnityBootstrapper
    {        
        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri("ModuleCatalog.xaml", UriKind.Relative));
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance<IPresentationController>(Container.Resolve<PresentationController>());
            Container.RegisterInstance<ShellViewModel>(Container.Resolve<ShellViewModel>());
        }
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
