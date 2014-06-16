using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Business.ServiceContracts;
using BillManager.Business.ServiceManagers;
using Core.MEF;
using NUnit.Framework;
using Core.Extensions;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using System.ComponentModel.Composition;

namespace BillManager.Test.ServiceManagerTest
{
    [TestFixture]
    public class ServiceManagerTest
    {
        [Test]
        public void CanOpenChannel()
        {
            ChannelFactory<IAccountService> channelFactory =
                   new ChannelFactory<IAccountService>("");

            IAccountService proxy = channelFactory.CreateChannel();

            (proxy as ICommunicationObject).Open();

            Account a = proxy.GetAccount("DangNguyen", "dangfinance");
            channelFactory.Close();
        }
        [Test]
        public void TestTestServiceManager()
        {
            MEF.Container = new BillManager.Business.Bootstapper().InitializeContainer();            
            MEF.Container.SatisfyImportsOnce(this);

            AccountServiceManager accountServiceManager = new AccountServiceManager();
            IEnumerable<Website> websites = accountServiceManager.GetAllPaymentSites(1);
            IEnumerator<Website> enumerator = websites.GetEnumerator();
            enumerator.MoveNext();
            Website w1 = enumerator.Current;
            int hash = w1.GetHashCode();

            Bill b = new Bill()
            {
                AccountId = 1,
                AccountNum = "23256537",
                BillFrequency = BillFrequency.Monthly,
                BillKind = BillKind.Paydown,
                CommenceDate = DateTime.Today,
                DayDueInMonth = 30,
                Description = "Best buy payments",
                IsActive = true,
                Name = "Best Buy",
                PhoneNum = "800 583-2893",
                AutopayIsEnrolled = true,
                PayOptions = new List<PayOption>()
            };

            b.PayOptions.Add(new PayOption() { IsPrimary = false, Label = "Chase", Website = w1, Bill = b });     
            
            Bill persisted = accountServiceManager.AddNewBill(b);
        }
        [Import]
        IDataRepositoryFactory dataRepositoryFactory;
    }
}

//FavoriteLink f = new FavoriteLink()
//{
//    AccountId = 1,
//    Label = "ChaseF",
//    Website = w
//};

//IFavoriteLinkRepository favoriteLinkRepository = dataRepositoryFactory.GetDataRepository<IFavoriteLinkRepository>();
//FavoriteLink persisted = favoriteLinkRepository.Add(f);

//IEnumerable<Website> websites = accountServiceManager.GetAllPaymentSites(1);
//IEnumerator<Website> enumerator = websites.GetEnumerator();
//enumerator.MoveNext();
//Website w = enumerator.Current;
//int hashc = w.GetHashCode();

//IWebsiteRepository websiteRepository = dataRepositoryFactory.GetDataRepository<IWebsiteRepository>();
//Website w1 = websiteRepository.Get(w.WebsiteId);
//int hash1 = w1.GetHashCode();

//Account account = accountServiceManager.GetAccount("d", "d");
//Assert.IsNotNull(account.FavoriteLinks);
//Assert.IsNotNull(account);                        
//IList<Bill> bills = accountServiceManager.GetAllBills(1).ToList();
//Category c = new Category()
//{
//    AccountId = 1,
//    Name = "Credit"
//};
//Category persisted = accountServiceManager.AddNewCategory(c);
//Website w = new Website()
//{
//    AccountId = 1,
//    Name = "Chase"
//};
//Website persistedWebsite = accountServiceManager.AddNewWebsite(w);


//Bill b = new Bill()
//{
//    AccountId = 1,
//    AccountNum = "777777",
//    BillFrequency = BillFrequency.Monthly,
//    BillKind = BillKind.Reoccurring,
//    CommenceDate = DateTime.Today,
//    DayDueInMonth = 12,
//    Description = "Test bill",
//    IsActive = true,
//    Name = "Test bill",
//    PhoneNum = "503 277-9483",
//    PayOptions = new List<PayOption>()
//    {
//        new PayOption() { IsPrimary = true, Label = "Chase", Website = w1 },
//        new PayOption() { IsPrimary = false, Label = "PGE", Website = w2 }
//    }, 
//    AutopayIsEnrolled = false
//};
//b.PayOptions.ForEach(p => p.Bill = b);