using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Interfaces;
using BillManager.Desktop.ViewModels;
using BillManager.Desktop.Views;
using Core.Exceptions;
using Core.ServiceModel.Contracts;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Moq;

namespace BillManager.Desktop
{
    public class LogonViewModel : ViewModelBase
    {
        IPresentationController controller;
        IServiceFactory serviceFactory;
        IUnityContainer container;
        IStartupManager startupManager;
        public LogonViewModel(IPresentationController controller, IServiceFactory serviceFactory, IUnityContainer container, IStartupManager startupManager)
        {
            this.controller = controller;
            this.serviceFactory = serviceFactory;
            this.container = container;
            this.startupManager = startupManager;
        }
        protected override void InitializeICommands()
        {
            LogonCommand = new DelegateCommand<object>(OnLogonCommand);
        }
        public ICommand LogonCommand { get; set; }
        void OnLogonCommand(object o)
        {
            startupManager.LoadLogin(userName, password);
            //userName = "DangNguyen";
            //password = "dangfinance";
            //UsingServiceClient<IAccountService>(serviceFactory.CreateServiceClient<IAccountService>(), accountServiceClient =>
            //{
            //    try
            //    {
            //        FaultHandledOperation(() =>
            //        {
            //            SessionData.Instance.Account = accountServiceClient.GetAccount(userName.ToLowerInvariant(), password);
            //            if (SessionData.Instance.Account != default(Account))
            //                LoadLogin();
            //        });
            //    }               
            //    catch (EndpointNotFoundException)
            //    {
            //        MessageBox.Show("Bill manager service is not available", "Service not available", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //});
        }
        //void LoadLogin()
        //{
        //    ShellViewModel shell = container.Resolve<ShellViewModel>();
        //    shell.IsLoading = true;

        //    Task.Factory.StartNew(() =>
        //    {
        //        container.RegisterInstance<NoBillSelectedView>(container.Resolve<NoBillSelectedView>());
        //        container.RegisterInstance<BreadCrumbView>(container.Resolve<BreadCrumbView>());
        //        container.RegisterInstance<ToolsViewModel>(container.Resolve<ToolsViewModel>());
        //        container.RegisterInstance<ToolsView>(container.Resolve<ToolsView>());
        //        container.RegisterInstance<BillListViewModel>(container.Resolve<BillListViewModel>());
        //        container.RegisterInstance<BillListView>(container.Resolve<BillListView>());
        //        container.RegisterInstance<BillContentViewModel>(container.Resolve<BillContentViewModel>());
        //    });
            
        //    shell.IsLoading = false;
        //    controller.RequestNavigate(RegionNames.ShellRegion, typeof(MainShell).Name);
        //}
        string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}


//catch (FaultException e)
//{
//    MessageBox.Show(e.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
//}