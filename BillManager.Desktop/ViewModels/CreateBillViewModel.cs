using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BillManager.Client.Model;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using Core.ServiceModel.Contracts;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Views;
using Microsoft.Practices.Prism.Regions;
using FluentValidation.Results;
using BillManager.Client.Enums;
using Microsoft.Practices.Unity;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using Core.Extensions;
using UI.Controls;
using BillManager.Desktop.ViewModels;
using Core;
using AutoMapper;
using Core.Interfaces;

namespace BillManager.Desktop
{
    public class CreateBillViewModel : ViewModelBase, INavigationAware
    {
        IServiceFactory serviceFactory;
        IPresentationController controller;
        IRegionManager regionManager;
        IUnityContainer container;
        IEventAggregator eventAggregator;
        public CreateBillViewModel(IServiceFactory serviceFactory, IPresentationController controller, IRegionManager regionManager, IUnityContainer container, IEventAggregator eventAggregator)
        {
            this.serviceFactory = serviceFactory;
            this.controller = controller;
            this.regionManager = regionManager;
            this.container = container;
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<NewCategoryAddedEvent>().Subscribe(OnNewCategoryAdded, true);
            eventAggregator.GetEvent<NewWebsiteAddedEvent>().Subscribe(OnNewWebsiteAdded, true);
                 
            //Temp comment
            UsingServiceClient<IAccountService>(serviceFactory.CreateServiceClient<IAccountService>(), accountService =>
            {   
                websites = new ObservableCollection<Website>();
                accountService.GetAllPaymentSites(SessionData.Instance.Account.AccountId).ForEach(w => websites.Add(EntityMapper.PropertyMap<Business.Entities.Website, Website>(w)));
                
                categories = new ObservableCollection<Category>();
                accountService.GetAllBillCategories(SessionData.Instance.Account.AccountId).ForEach(c => categories.Add(EntityMapper.PropertyMap<Business.Entities.Category, Category>(c)));
            });

            PayOptions = new ObservableCollection<PayOption>();
        }
        protected override void InitializeICommands()
        {            
            AddBillCommand = new DelegateCommand<object>(OnAddBillCommand);
            ClearCommand = new DelegateCommand<object>(OnClearCommand);
            CreateCategoryCommand = new DelegateCommand<object>(OnCreateCategoryCommand);
            CreateWebsiteCommand = new DelegateCommand<object>(OnCreateWebsiteCommand);
            PrimaryClickedCommand = new DelegateCommand<PayOption>(OnPrimaryClickedCommand);
            BackCommand = new DelegateCommand<object>(OnBackCommand);
            DeletePayOptionCommand = new DelegateCommand<PayOption>(OnDeletePayOptionCommand);
            WebsiteClickedCommand = new DelegateCommand<Website>(OnWebsiteClickedCommand);
        }
        
        public ICommand AddBillCommand { get; set; }        
        void OnAddBillCommand(object o)
        {
            ValidateEntity(Bill);

            if (Bill.IsValid)
            {
                inValidationMode = false;
                Bill.PayOptions = PayOptions.ToList();                                
                Business.Entities.Bill b = container.Resolve<IEntityMapper>().Map<Bill, Business.Entities.Bill>(Bill);
                b.PayOptions.ForEach(p => p.Bill = b);

                //Temp comment
                UsingServiceClient<IAccountService>(serviceFactory.CreateServiceClient<IAccountService>(), accountService =>
                {
                    accountService.AddNewBill(b);
                });
                
                OnBackCommand(new object());
                ClearBill();
                eventAggregator.GetEvent<BillUpdateRequiredEvent>().Publish(new object());
            }
            else
                inValidationMode = true;
        }
        bool inValidationMode;
        public ICommand ClearCommand { get; set; }
        void OnClearCommand(object o)
        {
            ClearBill();
        }
        void ClearBill()
        {
            bill = CreateNewBill();
            ValidationErrors = null;

            foreach (PayOption p in PayOptions)                        
                Websites.Add(p.Website);
            PayOptions.Clear();

            FirePropertyChanged("Name");
            FirePropertyChanged("Category");
            FirePropertyChanged("DayDueInMonth");
            FirePropertyChanged("Description");
            FirePropertyChanged("CommenceDate");
            FirePropertyChanged("AutopayIsEnrolled");
            FirePropertyChanged("PhoneNum");
            FirePropertyChanged("AccountNum");
            FirePropertyChanged("BillFrequency");
            FirePropertyChanged("ValidationErrors");
            FirePropertyChanged("PayOptions");
        }
        Bill bill;
        public Bill Bill
        {
            get { return bill ?? (bill = CreateNewBill()); }
        }
        Bill CreateNewBill()
        {
            return new Bill()
            {
                IsActive = true,
                AccountId = SessionData.Instance.Account.AccountId
            };
        }
        public ICommand BackCommand { get; set; }
        void OnBackCommand(object o)
        {
            eventAggregator.GetEvent<FlyoutBackNavigationEvent>().Publish(string.Empty);
        }

        # region Data fields

        public string Name
        {
            get { return Bill.Name; }
            set
            {
                Bill.Name = value;
                if (inValidationMode) ValidateEntity(Bill);
                FirePropertyChanged("Name");
            }
        }
        public int? DayDueInMonth
        {
            get { return Bill.DayDueInMonth; }
            set
            {
                if (Bill.DayDueInMonth != value)
                {
                    Bill.DayDueInMonth = value;
                    if (inValidationMode) ValidateEntity(Bill);
                    FirePropertyChanged("DayDueInMonth");
                }
            }
        }
        public string Description
        {
            get { return Bill.Description; }
            set { Bill.Description = value; }
        }
        public DateTime CommenceDate
        {
            get { return Bill.CommenceDate; }
            set { Bill.CommenceDate = value; }
        }
        public bool AutopayIsEnrolled
        {
            get { return Bill.AutopayIsEnrolled; }
            set { Bill.AutopayIsEnrolled = value; }
        }
        public string PhoneNum
        {
            get { return Bill.PhoneNum; }
            set { Bill.PhoneNum = value; }
        }
        public string AccountNum
        {
            get { return Bill.AccountNum; }
            set { Bill.AccountNum = value; }
        }
        int billFrequencySelectedIndex = -1;
        public int BillFrequencySelectedIndex
        {
            get { return billFrequencySelectedIndex; }
            set 
            { 
                billFrequencySelectedIndex = value;
                FirePropertyChanged("BillFrequencySelectedIndex");
            }
        }
        public BillFrequency BillFrequency
        {
            get 
            { 
                if (Bill.BillFrequency == default(BillFrequency))
                   BillFrequencySelectedIndex = -1;
                
                return Bill.BillFrequency; 
            }
            set 
            { 
                Bill.BillFrequency = value;
                if (inValidationMode) ValidateEntity(Bill);
                FirePropertyChanged("BillFrequency");
            }
        }
        ObservableCollection<string> billFrequencyList;
        public ObservableCollection<string> BillFrequencyList
        {
            get { return billFrequencyList ?? (billFrequencyList = new ObservableCollection<string>(Enum.GetNames(typeof(BillFrequency)))); }
        }
        int billKindSelectedIndex = -1;
        public int BillKindSelectedIndex
        {
            get { return billKindSelectedIndex; }
            set 
            { 
                billKindSelectedIndex = value;
                FirePropertyChanged("BillKindSelectedIndex");
            }
        }        
        public BillKind BillKind
        {
            get 
            {
                if (Bill.BillKind == default(BillKind))
                    BillKindSelectedIndex = -1;

                return Bill.BillKind; 
            }
            set 
            { 
                Bill.BillKind = value;
                if (inValidationMode) ValidateEntity(Bill);
                FirePropertyChanged("BillKind");
            }
        }        
        ObservableCollection<string> billKindList;
        public ObservableCollection<string> BillKindList
        {
            get { return billKindList ?? (billKindList = new ObservableCollection<string>(Enum.GetNames(typeof(BillKind)))); }
        }        

        ObservableCollection<Website> websites;
        public ObservableCollection<Website> Websites
        {
            get { return websites; }
        }
        Website selectedWebsite;
        public Website SelectedWebsite
        {
            get { return selectedWebsite; }
            set
            {
                if (selectedWebsite != value)
                {
                    selectedWebsite = value;
                    AddPayOption(value);                    
                }
            }
        }
        void OnNewWebsiteAdded(Website website)
        {
            AddPayOption(website);
        }
        void AddPayOption(Website website)
        {            
            if (PayOptions.FirstOrDefault(p => p.Website == website) == default(PayOption))
            {
                PayOption p = new PayOption(website, Bill) { Label = website.Name };                
                if (PayOptions.Count == 0)
                    p.IsPrimary = true;
                PayOptions.Add(p);
                Websites.Remove(website);
                FirePropertyChanged("PayOptions");
            }
        }
        public ICommand PrimaryClickedCommand { get; set; }
        void OnPrimaryClickedCommand(PayOption payOption)
        {
            PayOptions.ForEach(p =>
            {
                if (p != payOption)
                    p.IsPrimary = false;
            });
        }
        public ICommand WebsiteClickedCommand { get; set; }
        void OnWebsiteClickedCommand(Website website)
        {
            AddPayOption(website);
        }
        public ICommand DeletePayOptionCommand { get; set; }
        void OnDeletePayOptionCommand(PayOption payOption)
        {
            bool isPrimary = payOption.IsPrimary;

            PayOptions.Remove(payOption);
            Websites.Add(payOption.Website);

            if (PayOptions.Count > 0 && isPrimary)
                PayOptions[0].IsPrimary = true;
            else if (PayOptions.Count == 0)
                FirePropertyChanged("PayOptions");
        }
        ObservableCollection<PayOption> payOptions;
        public ObservableCollection<PayOption> PayOptions
        {
            get { return payOptions; }
            set { payOptions = value; }
        }        
        public ICommand CreateWebsiteCommand { get; set; }
        void OnCreateWebsiteCommand(object o)
        {            
            controller.RequestNavigate<ShellViewModel>(RegionNames.FlyoutRegion, typeof(CreateWebsiteView).Name, v =>
            {
                v.FlyoutContentWidth = 420;
                v.ShadeAdornerIsVisible = true;
            });
        }        
        ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }
        public Category Category
        {
            get { return bill.Category; }
            set 
            {
                if (bill.Category != value)
                {
                    bill.Category = value;
                    FirePropertyChanged("Category");
                }
            }
        }
        void OnNewCategoryAdded(Category category)
        {
            Categories.Add(category);
            Category = category;
        }
        public ICommand CreateCategoryCommand { get; set; }
        void OnCreateCategoryCommand(object o)
        {
            controller.RequestNavigate<ShellViewModel>(RegionNames.FlyoutRegion, typeof(CreateCategoryView).Name, v =>
            {
                v.FlyoutContentWidth = 300;
                v.ShadeAdornerIsVisible = true;
            });            
        }
        
        # endregion

        # region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        # endregion
    }
}














//controller.RequestOpenWindowModal<MetroWindow>(typeof(ViewContainerWindow), w =>
//{
//    w.Title = "Create new pay option";
//    w.Height = 250;
//    w.Width = 420;
//    w.ResizeMode = ResizeMode.NoResize;
//    w.ShowMaxRestoreButton = false;
//    w.ShowMinButton = false;
//}, (s, e) =>
//{
//    controller.RequestNavigate(RegionNames.ViewContainerRegion, typeof(CreateWebsiteView).Name);
//});

//controller.RequestOpenWindowModal<MetroWindow>(typeof(ViewContainerWindow), w =>
//{
//    w.Title = "Create new category";
//    w.Height = 130;
//    w.Width = 300;
//    w.ResizeMode = ResizeMode.NoResize;
//    w.ShowMaxRestoreButton = false;
//    w.ShowMinButton = false;
//}, (s, e) =>
//{
//    controller.RequestNavigate(RegionNames.ViewContainerRegion, typeof(CreateCategoryView).Name);
//});