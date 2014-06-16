using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Views;
using Core;
using Core.ServiceModel.Contracts;
using Core.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace BillManager.Desktop.ViewModels
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        IServiceFactory serviceFactory;
        IPresentationController controller;
        IEventAggregator eventAggregator;
        public CreateCategoryViewModel()
        {
            this.serviceFactory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            this.controller = ServiceLocator.Current.GetInstance<IPresentationController>();
            this.eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
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
            ValidateEntity(Category);

            if (Category.IsValid)
            {
                Category persistedCategory = default(Category);
                //Temp comment
                UsingServiceClient<IAccountService>(serviceFactory.CreateServiceClient<IAccountService>(), accountService =>
                {
                    persistedCategory = EntityMapper.PropertyMap<Business.Entities.Category, Category>(
                        accountService.AddNewCategory(EntityMapper.PropertyMap<Category, Business.Entities.Category>(Category)));
                });

                if (persistedCategory != default(Category))
                {
                    eventAggregator.GetEvent<NewCategoryAddedEvent>().Publish(persistedCategory);
                    eventAggregator.GetEvent<FlyoutBackNavigationEvent>().Publish(typeof(CreateBillView).Name);
                    Reset();
                }
                else
                    MessageBox.Show("Category was not created!", "Error creating category", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }        
        public string Name
        {
            get { return Category.Name; }
            set { Category.Name = value; }
        }
        Category category;
        public Category Category
        {
            get { return category ?? (category = new Category() { AccountId = SessionData.Instance.Account.AccountId }); }
        }
        void Reset()
        {
            category = new Category() { AccountId = SessionData.Instance.Account.AccountId };
            FirePropertyChanged("Name");
        }
    }
}
