using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using BillManager.Desktop.Views;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using Microsoft.Practices.ServiceLocation;
using Core.ServiceModel.Contracts;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Core;

namespace BillManager.Desktop
{
    public class CreateWebsiteViewModel : ViewModelBase
    {
        IPresentationController controller;
        IEventAggregator eventAggregator;
        public CreateWebsiteViewModel(IPresentationController controller, IEventAggregator eventAggregator)
        {
            this.controller = controller;
            this.eventAggregator = eventAggregator;
        }
        protected override void InitializeICommands()
        {
            BackCommand = new DelegateCommand<object>(OnBackCommand);
            CreateCommand = new DelegateCommand<object>(OnCreateCommand);
        }
        public ICommand BackCommand { get; set; }
        void OnBackCommand(object o)
        {            
            eventAggregator.GetEvent<FlyoutBackNavigationEvent>().Publish(typeof(CreateBillView).Name);
        }
        public ICommand CreateCommand { get; set; }
        void OnCreateCommand(object o)
        {
            ValidateEntity(Website);

            if (Website.IsValid)
            {
                Website persistedSite = default(Website);
                //Temp comment
                UsingServiceClient<IAccountService>(ServiceLocator.Current.GetInstance<IServiceFactory>().CreateServiceClient<IAccountService>(), accountService =>
                {
                    persistedSite = EntityMapper.PropertyMap<Business.Entities.Website, Website>(
                        accountService.AddNewWebsite(EntityMapper.PropertyMap<Website, Business.Entities.Website>(Website)));
                });

                if (persistedSite != default(Website))
                {
                    ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NewWebsiteAddedEvent>().Publish(persistedSite);
                    OnBackCommand(new object());
                    Reset();
                }
                else
                    MessageBox.Show("Category was not created!", "Error creating category", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            
            inValidationMode = true;
        }
        bool inValidationMode;        
        Website website;
        public Website Website
        {
            get { return website ?? (website = new Website() { AccountId = SessionData.Instance.Account.AccountId }); }
        }
        void Reset()
        {
            website = new Website() { AccountId = SessionData.Instance.Account.AccountId };
            FirePropertyChanged("Name");
            FirePropertyChanged("Description");
            FirePropertyChanged("UrlString");
            FirePropertyChanged("UserName");
            FirePropertyChanged("Password");
        }
        public string Name
        {
            get { return Website.Name; }
            set 
            { 
                Website.Name = value;
                if (inValidationMode) ValidateEntity(Website);
                FirePropertyChanged("Name");
            }
        }        
        public string Description
        {
            get { return Website.Description; }
            set { Website.Description = value; }
        }        
        public string UrlString
        {
            get { return Website.UrlString; }
            set 
            { 
                Website.UrlString = value;
                if (inValidationMode) ValidateEntity(Website);
                FirePropertyChanged("UrlString");
            }
        }
        public string UserName
        {
            get { return Website.UserName; }
            set { Website.UserName = value; }
        }
        public string Password
        {
            get { return Website.Password; }
            set { Website.Password = value; }
        }
    }
}
