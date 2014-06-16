using Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BillManager.Client.Model;
using Core.ServiceModel.Contracts;
using BillManager.Client.ServiceContracts;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using BillManager.Desktop.Views;
using BillManager.Desktop.Interfaces;
using System.Windows.Data;
using BillManager.Client.Enums;
using Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using UI.Controls;
using BillManager.Desktop.ViewModels;
using System.Diagnostics;
using Core.Utils;

namespace BillManager.Desktop
{
    public class BillListViewModel : ViewModelBase
    {
        IPaymentController paymentController;
        IEventAggregator eventAggregator;
        IPresentationController controller;
        public BillListViewModel(IPaymentController paymentController, IEventAggregator eventAggregator, IPresentationController controller)
        {
            this.paymentController = paymentController;
            this.eventAggregator = eventAggregator;
            this.controller = controller;

            UpdateBills();
            this.eventAggregator.GetEvent<BillUpdateRequiredEvent>().Subscribe((o) => UpdateBills(), true);
            this.eventAggregator.GetEvent<BillSortRequiredEvent>().Subscribe((o) => SortBills(), true);            
        }
        void UpdateBills()
        {
            Bills = new ObservableCollection<Bill>(paymentController.GetCurrentBills());
            eventAggregator.GetEvent<BillLoadedEvent>().Publish(Bills);
        }
        void SortBills()
        {
            Bills.ForEach(b => b.ResetAnimation());

            Bills = new ObservableCollection<Bill>(bills.OrderBy(b => b.BillStatus).ThenBy(b =>
            {
                int days = b.DueDate.Subtract(DateTime.Today.Date).Days;
                if (b.BillStatus == BillStatus.Paid)
                    return -days;
                else
                    return days;
            }));
        }
        protected override void InitializeICommands()
        {
            PayNowCommand = new DelegateCommand<Bill>(OnPayNowCommand);
            BillSelectedCommand = new DelegateCommand<Bill>(OnBillSelectedCommand);
            NavigateToAddBillCommand = new DelegateCommand<object>(OnNavigateToAddBillCommand);
        }
        ObservableCollection<Bill> bills;
        public ObservableCollection<Bill> Bills
        {
            get { return bills; }
            set 
            { 
                bills = value;
                FirePropertyChanged("Bills");
            }
        }
        public ICommand PayNowCommand { get; set; }
        void OnPayNowCommand(Bill bill)
        {
            SelectedBill = bill;            
            Process.Start(BrowserHelper.GetDefaultBrowserPath(), bill.PayOptions.First().Website.Url.ToString());
        }
        public ICommand BillSelectedCommand { get; set; }
        void OnBillSelectedCommand(Bill bill)
        {
            if (bill.IsPaid)
                controller.RequestNavigate(RegionNames.ContentRegion, typeof(BillSummaryView).Name);
            else
                controller.RequestNavigate(RegionNames.ContentRegion, typeof(BillContentView).Name);

            eventAggregator.GetEvent<BillSelectionChangedEvent>().Publish(bill);
        }
        Bill selectedBill;
        public Bill SelectedBill
        {
            get { return selectedBill; }
            set 
            { 
                selectedBill = value;
                FirePropertyChanged("SelectedBill");
            }
        }
        public ICommand NavigateToAddBillCommand { get; set; }
        void OnNavigateToAddBillCommand(object o)
        {            
            controller.RequestNavigate<ShellViewModel>(RegionNames.FlyoutRegion, typeof(CreateBillView).Name, v =>
            {
                v.FlyoutContentWidth = 600;
                v.ShadeAdornerIsVisible = true;
            });
        }
    }
}