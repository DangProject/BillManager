using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Interfaces;
using BillManager.Desktop.ViewModels;
using BillManager.Desktop.Views;
using Core;
using Core.Interfaces;
using Core.ServiceModel.Contracts;
using Core.UI;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace BillManager.Desktop
{
    public class StartupManager : ServiceClientConsumerBase, IStartupManager
    {
        IPresentationController controller;
        IServiceFactory serviceFactory;
        IUnityContainer container;
        IRegionManager regionManager;
        ShellViewModel shell;
        public StartupManager(IPresentationController controller, IServiceFactory serviceFactory, IUnityContainer container, IRegionManager regionManager)
        {
            this.controller = controller;
            this.serviceFactory = serviceFactory;
            this.container = container;
            this.regionManager = regionManager;

            shell = container.Resolve<ShellViewModel>();
        }

        TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();        
        public void LoadLogin(string userName, string password)
        {
            shell.IsLoading = true;
            Task.Factory.StartNew(() =>
            {
                ErrorType errorType = ErrorType.None;
                string message = string.Empty;
                UsingServiceClient<IAccountService>(serviceFactory.CreateServiceClient<IAccountService>(), accountServiceClient =>
                {
                    try
                    {
                        //Temp comment
                        SessionData.Instance.Account = EntityMapper.PropertyMap<Business.Entities.Account, Account>(accountServiceClient.GetAccount(userName.ToLowerInvariant(), password));
                        if (SessionData.Instance.Account != default(Account))
                            LoadNonUIComponents();
                    }
                    catch (FaultException e)
                    {
                        errorType = ErrorType.General;
                        message = e.Message;
                    }
                    catch (EndpointNotFoundException)
                    {
                        errorType = ErrorType.NoService;
                    }
                });

                switch (errorType)
                {
                    case ErrorType.None:
                        return new Tuple<ErrorType, string>(errorType, string.Empty);
                    case ErrorType.NoService:
                        return new Tuple<ErrorType, string>(errorType, string.Empty);
                    case ErrorType.General:
                        return new Tuple<ErrorType, string>(errorType, message);
                    default:
                        return new Tuple<ErrorType, string>(errorType, string.Empty);
                }                
            }).ContinueWith(t => 
            {
                switch (t.Result.Item1)
                {
                    case ErrorType.None:
                        NavigateToMain();
                        break;
                    case ErrorType.NoService:
                        DisplayNoServiceError();
                        break;
                    case ErrorType.General:
                        DisplayErrorMessage(t.Result.Item2);
                        break;
                    default:
                        NavigateToMain();
                        break;
                }
            }, uiScheduler);
        }

        void NavigateToMain()
        {
            LoadUIComponents();
            shell.IsLoading = false;
            controller.RequestNavigate(RegionNames.ShellRegion, typeof(MainShell).Name);
        }
        void DisplayErrorMessage(string message)
        {
            shell.IsLoading = false;
            MessageBox.Show(message, "Service error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void DisplayNoServiceError()
        {
            shell.IsLoading = false;            
            MessageBox.Show("Bill manager service is not available", "Service not available", MessageBoxButton.OK, MessageBoxImage.Error);
        }        
        void LoadNonUIComponents()
        {
            EntityMapper mapper = new EntityMapper();
            mapper.Configure<Business.Entities.Category, Category>();
            mapper.Configure<Business.Entities.PayOption, PayOption>();
            mapper.Configure<Business.Entities.Website, Website>();
            mapper.Configure<Category, Business.Entities.Category>();
            mapper.Configure<PayOption, Business.Entities.PayOption>();
            mapper.Configure<Website, Business.Entities.Website>();
            container.RegisterInstance<IEntityMapper>(mapper);
            container.RegisterInstance<IPaymentController>(container.Resolve<PaymentController>());
            container.RegisterInstance<ToolsViewModel>(container.Resolve<ToolsViewModel>());
            container.RegisterInstance<NoBillSelectedViewModel>(container.Resolve<NoBillSelectedViewModel>()); 
            container.RegisterInstance<BillListViewModel>(container.Resolve<BillListViewModel>());
            container.RegisterInstance<BillContentViewModel>(container.Resolve<BillContentViewModel>());
            container.RegisterInstance<CreateBillViewModel>(container.Resolve<CreateBillViewModel>());
            container.RegisterInstance<MainShellViewModel>(container.Resolve<MainShellViewModel>());
            container.RegisterInstance<BillSummaryViewModel>(container.Resolve<BillSummaryViewModel>());                 
            
            regionManager.RegisterViewWithRegion(RegionNames.BreadCrumbRegion, typeof(BreadCrumbView));
            regionManager.RegisterViewWithRegion(RegionNames.ToolsRegion, typeof(ToolsView));
            regionManager.RegisterViewWithRegion(RegionNames.ListRegion, typeof(BillListView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(NoBillSelectedView));
            //regionManager.RegisterViewWithRegion(RegionNames.AddBillRegion, typeof(CreateBillView));
        }
        void LoadUIComponents()
        {
            container.RegisterInstance<NoBillSelectedView>(container.Resolve<NoBillSelectedView>());
            container.RegisterInstance<BreadCrumbView>(container.Resolve<BreadCrumbView>());
            container.RegisterInstance<ToolsView>(container.Resolve<ToolsView>());
            container.RegisterInstance<BillListView>(container.Resolve<BillListView>());
            //container.RegisterInstance<CreateCategoryView>(container.Resolve<CreateCategoryView>());
            //container.RegisterInstance<CreateWebsiteView>(container.Resolve<CreateWebsiteView>());
            //container.RegisterInstance<ViewContainerWindow>(container.Resolve<ViewContainerWindow>());
        }
        public enum ErrorType
        {
            None,
            NoService,
            General
        }
    }
}










//mapper.Configurations.Add(typeof(Business.Entities.Category), obj => mapper.Map<Category, Business.Entities.Category>(obj as Category));
//mapper.Configurations.Add(typeof(Business.Entities.PayOption), obj => mapper.Map<PayOption, Business.Entities.PayOption>(obj as PayOption));
//mapper.Configurations.Add(typeof(Business.Entities.Website), obj => mapper.Map<Website, Business.Entities.Website>(obj as Website));
//mapper.Configurations.Add(typeof(Business.Entities.Bill), obj => mapper.Map<Bill, Business.Entities.Bill>(obj as Bill));

            //container.RegisterInstance<NoBillSelectedView>(container.Resolve<NoBillSelectedView>());
            //container.RegisterInstance<BreadCrumbView>(container.Resolve<BreadCrumbView>());
            //container.RegisterInstance<ToolsViewModel>(container.Resolve<ToolsViewModel>());
            ////container.RegisterInstance<ToolsView>(container.Resolve<ToolsView>());
            //container.RegisterInstance<BillListViewModel>(container.Resolve<BillListViewModel>());
            ////container.RegisterInstance<BillListView>(container.Resolve<BillListView>());
            //container.RegisterInstance<BillContentViewModel>(container.Resolve<BillContentViewModel>());