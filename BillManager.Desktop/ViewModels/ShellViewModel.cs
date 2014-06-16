using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using UI.Controls;
using Core.Extensions;
using Microsoft.Practices.Prism.Events;
using System.Windows.Controls;

namespace BillManager.Desktop.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        IRegionManager regionManager;
        IPresentationController controller;
        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IPresentationController controller)
        {
            this.regionManager = regionManager;
            this.controller = controller;

            eventAggregator.GetEvent<FlyoutBackNavigationEvent>().Subscribe(OnBackCommand, true);
        }
        protected override void InitializeICommands()
        {
            BackCommand = new DelegateCommand<string>(OnBackCommand);
        }
        bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set 
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    FirePropertyChanged("IsLoading");
                }
            }
        }
        bool shadeAdornerIsVisible;
        public bool ShadeAdornerIsVisible
        {
            get     { return shadeAdornerIsVisible; }
            set 
            { 
                shadeAdornerIsVisible = value;
                FirePropertyChanged("ShadeAdornerIsVisible");
            }
        }      
        double flyoutContentWidth;
        public double FlyoutContentWidth
        {
            get { return flyoutContentWidth; }
            set 
            { 
                flyoutContentWidth = value;
                FirePropertyChanged("FlyoutContentWidth");
            }
        }
        public ICommand BackCommand { get; set; }
        void OnBackCommand(string viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
            {
                controller.RequestNavigate<ShellViewModel>(RegionNames.FlyoutRegion, viewName, v =>
                {
                    v.FlyoutContentWidth = 600;
                    v.ShadeAdornerIsVisible = true;
                });
            }
            else
            {
                regionManager.Regions[RegionNames.FlyoutRegion].ActiveViews.ForEach(v => regionManager.Regions[RegionNames.FlyoutRegion].Remove(v));
                this.ShadeAdornerIsVisible = false;
            }
        }
    }
}
