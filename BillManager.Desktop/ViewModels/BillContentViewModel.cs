using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BillManager.Client.Enums;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Interfaces;
using Core;
using Core.ServiceModel.Contracts;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System.Diagnostics;
using Core.Utils;
using BillManager.Desktop.Views;

namespace BillManager.Desktop
{
    public class BillContentViewModel : ViewModelBase
    {
        IServiceFactory serviceFactory;
        IPaymentController paymentContoller;
        IEventAggregator eventAggregator;
        IPresentationController controller;
        public BillContentViewModel(IServiceFactory serviceFactory, IPaymentController paymentContoller, IEventAggregator eventAggregator, 
                                    IPresentationController controller)
        {
            this.serviceFactory = serviceFactory;
            this.paymentContoller = paymentContoller;
            this.eventAggregator = eventAggregator;
            this.controller = controller;

            eventAggregator.GetEvent<BillSelectionChangedEvent>().Subscribe(OnBillSelectionChanged, true);
        }
        protected override void InitializeICommands()
        {
            MarkPaidCommand = new DelegateCommand(OnMarkPaidCommand);
            GoToSiteCommand = new DelegateCommand<Uri>(OnGoToSiteCommand);
        }
        Bill bill;
        public Bill Bill
        {
            get { return bill; }
            set 
            {
                if (bill != value)
                {
                    bill = value;                    
                    FirePropertyChanged(() => Bill);
                }
            }
        }
        void OnBillSelectionChanged(Bill bill)
        {
            Bill = bill;
            ResetInput();
            if (bill.IsPaid && bill.LastPayment != default(Payment))
            {
                Amount = bill.LastPayment.Amount;
                PaymentDate = bill.LastPayment.DatePaid;
                Comment = bill.LastPayment.Comment;
            }
        }        
        public ICommand MarkPaidCommand { get; set; }
        void OnMarkPaidCommand()
        {
            Payment persistedPayment = null;

            if (bill != null)
            {
                // validate data first before sending to service
                Business.Entities.Payment payment = new Business.Entities.Payment()
                {
                    Amount = amount.Value,
                    BillId = bill.BillId,
                    Comment = comment,
                    PaymentMonthApplied = bill.DueDate.Date,
                    DatePaid = paymentDate.Value,
                    IsLate = (bill.DaysLeft < 0)
                };
                
                UsingServiceClient(serviceFactory.CreateServiceClient<IPaymentService>(), paymentService =>
                {
                    try
                    {
                        //Temp comment
                        persistedPayment = EntityMapper.PropertyMap<Business.Entities.Payment, Payment>(paymentService.MakePayment(payment));
                    }
                    catch (FaultException e)
                    {
                        MessageBox.Show(string.Format("Error saving payment\n,{0}", e.Message));
                    }
                });

                if (persistedPayment != null)
                {
                    if (persistedPayment.PaymentId == default(int))
                        MessageBox.Show("Payment did not get saved!");
                    else
                        goto success;
                }
                else
                    goto success;
            }

            return;
            
            success:
            paymentContoller.UpdateBill(bill, persistedPayment);
            eventAggregator.GetEvent<BillSortRequiredEvent>().Publish(new object());

            bill.NotifyPropertyChanges();
            if (!bill.IsPaid)
                ResetInput();
            else
                controller.RequestNavigate(RegionNames.ContentRegion, typeof(BillSummaryView).Name);
        }
        void ResetInput()
        {
            Amount = null;
            Comment = string.Empty;

            BillStatus status = bill.BillStatus;
            if (status == BillStatus.NoBillThisMonth || status == BillStatus.CantStart)
                PaymentDate = null;
            else
                PaymentDate = DateTime.Today;
        }
        public ICommand GoToSiteCommand { get; set; }
        void OnGoToSiteCommand(Uri url)
        {            
            Process.Start(BrowserHelper.GetDefaultBrowserPath(), url.ToString());
        }        
        decimal? amount;
        public decimal? Amount
        {
            get { return amount; }
            set 
            { 
                amount = value;
                FirePropertyChanged("Amount");
            }
        }        
        string comment;
        public string Comment
        {
            get { return comment; }
            set 
            { 
                comment = value;
                FirePropertyChanged(() => Comment);
            }
        }
        DateTime? paymentDate;
        public DateTime? PaymentDate
        {
            get { return paymentDate; }
            set 
            {   
                paymentDate = value;
                FirePropertyChanged(() => PaymentDate);
            }
        }
    }
}










//public string NoBillMessage
//{
//    get
//    {
//        if (bill != null)
//        {
//            switch (bill.BillStatus)
//            {
//                case BillStatus.Late:
//                    return string.Empty;
//                case BillStatus.NotPaid:
//                    return string.Empty;
//                case BillStatus.Paid:
//                    return "PAID";
//                case BillStatus.NoBillThisMonth:
//                    return "No bill";
//                case BillStatus.CantStart:
//                    return string.Format("Bill will being in {0}", bill.CommenceDate.ToString("MMMM"));
//                default:
//                    return string.Empty;
//            }
//        }

//        return string.Empty;
//    }
//}