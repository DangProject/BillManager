using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Enums;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using Core.ServiceModel.Contracts;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Moq;

namespace BillManager.Client
{
    public class Module : IModule
    {
        IUnityContainer container;
        public Module(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            //SetupMocks();
            //container.RegisterInstance<IServiceFactory>(mockServiceFactory.Object);

            container.RegisterInstance<IServiceFactory>(container.Resolve<ServiceFactory>());
        }
        Mock<IServiceFactory> mockServiceFactory;
        void SetupMocks()
        {
            Account account = new Account()
            {
                FirstName = "Dang",
                LastName = "Nguyen",
                AccountId = 1,
                UserName = "DangNguyen",
                Password = "dangfinance",
                Email = "account from mock"
            };

            IList<Bill> bills = new List<Bill>()
            {
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 1,
                    Name = "PGE",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.Monthly,
                    IsActive = true,
                    //DueDate = DateTime.Today.AddDays(-3)
                },
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 2,
                    Name = "Sunrise Water",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.BiMonthly,
                    IsActive = true,
                    //DueDate = DateTime.Today.AddDays(-2)
                },
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 3,
                    Name = "NW Natural Gas",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.Monthly,
                    IsActive = true,
                    //DueDate = DateTime.Today.AddDays(-1)
                },
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 4,
                    Name = "Frontier FIOS",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.Monthly,
                    IsActive = true,
                    //DueDate = DateTime.Today
                },
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 5,
                    Name = "T-Mobile",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.Monthly,
                    AutopayIsEnrolled = true,
                    IsActive = true,
                    //DueDate = DateTime.Today.AddDays(1)
                },
                new Bill() 
                {
                    AccountId = 1,
                    BillId = 6,
                    Name = "Gieco",
                    BillKind = BillKind.Reoccurring,
                    BillFrequency = BillFrequency.SemiYearly,
                    AutopayIsEnrolled = true,
                    IsActive = true,
                    //DueDate = DateTime.Today.AddDays(2)
                }
                //new Bill() 
                //{
                //    AccountId = 1,
                //    BillId = ,
                //    Name = "",
                //    BillKind = Core.BillKind.Reoccurring,
                //    BillFrequency = Core.BillFrequency
                //    IsActive = true
                //},
            };

            Mock<IAccountService> mockAccountService = new Mock<IAccountService>();
            //mockAccountService.Setup(mock => mock.GetAccount("dangnguyen", "dangfinance")).Returns(account);
            //mockAccountService.Setup(mock => mock.GetAllActiveBills(1)).Returns(bills);

            mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateServiceClient<IAccountService>()).Returns(mockAccountService.Object);
            //mockServiceFactory.Setup(mock => mock.CreateServiceClient<IAccountService>().GetAccount("dangnguyen", "dangfinance")).Returns(account);
        }
    }
}
