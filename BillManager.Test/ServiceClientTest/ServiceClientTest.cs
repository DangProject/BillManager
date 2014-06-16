using BillManager.Client;
using BillManager.Client.ServiceContracts;
using Core.ServiceModel.Contracts;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BillManager.Client.ServiceProxies;
//using BillManager.Client.Model;
using BillManager.Business.Entities;

namespace BillManager.Test.ServiceClientTest
{
    [TestFixture]
    class ServiceClientTest
    {
        IUnityContainer container;
        [TestFixtureSetUp]
        public void Setup()
        {         
            container = new UnityContainer();
            container.RegisterInstance<IServiceFactory>(container.Resolve<ServiceFactory>());         
        }
        [Test]
        public void CanAddBillWithObjectGraph()
        {
            IServiceFactory serviceFactory = container.Resolve<IServiceFactory>();            
            IAccountService accountService = serviceFactory.CreateServiceClient<IAccountService>();
            
            IEnumerable<Website> websites = accountService.GetAllPaymentSites(1);
            IEnumerator<Website> enumerator = websites.GetEnumerator();
            enumerator.MoveNext();
            Website w1 = enumerator.Current;
            int hash = w1.GetHashCode();

            Bill b = new Bill()
            {
                AccountId = 1,
                AccountNum = "sg25638",
                BillFrequency = BillFrequency.Monthly,
                BillKind = BillKind.Reoccurring,
                CommenceDate = DateTime.Today,
                DayDueInMonth = 25,
                Description = "Auto Ins",
                IsActive = true,
                Name = "Geico",
                PhoneNum = "800 893-7793",
                AutopayIsEnrolled = true,
                PayOptions = new List<PayOption>()
            };

            b.PayOptions.Add(new PayOption() { IsPrimary = false, Label = "Chase", Bill = b, Website = w1 });
            accountService.AddNewBill(b);
        }
        [Test]
        public void CanGetPaymentServiceClient()
        {
            IServiceFactory serviceFactory = container.Resolve<IServiceFactory>();
            IPaymentService proxy = serviceFactory.CreateServiceClient<IPaymentService>();
            Assert.IsTrue(proxy is IPaymentService);

            IEnumerable<Payment> payments = proxy.GetAllPaymentsByBill(1);
        }
        [Test]
        public void TestTestServiceClient()
        {
            IServiceFactory serviceFactory = container.Resolve<IServiceFactory>();
            IAccountService proxy = serviceFactory.CreateServiceClient<IAccountService>();
            Assert.IsTrue(proxy is IAccountService);

            Account account = proxy.GetAccount("DevinJones", "devinjones");

            account.Email = "herman@gmail.com";
            account.FirstName = "Herman";
            account.LastName = "Michaels";
            account.UserName = "HermanMichaels";
            account.Password = "hermanmichaels";

            Account updatedAccount = proxy.UpdateAccount(account);
        }
        [Test]
        public void CanObtainClientService()
        {
            IServiceFactory serviceFactory = container.Resolve<IServiceFactory>();
            
            IAccountService accountServiceProxy = serviceFactory.CreateServiceClient<IAccountService>();
            
            Assert.IsTrue(accountServiceProxy is IAccountService);
            
            Account account = accountServiceProxy.GetAccount("DangNguyen", "dangfinance");            
        }
        [Test]
        public void CanOpenClientProxy()
        {
            AccountServiceClient proxy = new AccountServiceClient();

            proxy.Open();

            Account account = proxy.GetAccount("DangNguyen", "dangfinance");
            Assert.IsNotNull(account);
        }       
    }
}
