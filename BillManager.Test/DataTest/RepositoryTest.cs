using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Business.ServiceManagers;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using Core.MEF;
using NUnit.Framework;
using System.ComponentModel.Composition;
using System.Data.Entity;
using BillManager.Data;

namespace BillManager.Test.DataTest
{
    [TestFixture]
    public class RepositoryTest
    {
        [Import]
        IDataRepositoryFactory dataRepositoryFactory;
        [TestFixtureSetUp]
        public void Setup()
        {
            MEF.Container = new BillManager.Business.Bootstapper().InitializeContainer();
            MEF.Container.SatisfyImportsOnce(this);
        }
        [Test]
        public void TestLoadOfChildObjects()
        {
            IBillRepository billRepo = dataRepositoryFactory.GetDataRepository<IBillRepository>();
            IEnumerable<Bill> bills = billRepo.GetAllActiveBills(1);
        }
        [Test]
        public void TestInsertPayOption()
        {   
            Database.SetInitializer(new MyInitializer());

            IWebsiteRepository webRepo = dataRepositoryFactory.GetDataRepository<IWebsiteRepository>();
            IEnumerable<Website> websites = webRepo.GetAllPaymentSites(1);
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

            IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();
            Bill persisted = billRepository.Add(b);
        }
    }
}


//Website w = new Website()
//{
//    AccountId = 1,
//    Description = "Chase bill pay",
//    Name = "Chase",
//    Password = "e",
//    UrlString = "www.Chase.com",
//    UserName = "e"
//};

//AccountServiceManager accountServiceManager = new AccountServiceManager();
//IEnumerable<Website> websites = accountServiceManager.GetAllPaymentSites(1);


//Website Wpersisted = webRepo.Add(w);

//                PayOptions = new List<PayOption>()
//                {
//                    new PayOption() { IsPrimary = true, Label = "Chase", Website = w1 },
//                    new PayOption() { IsPrimary = false, Label = "PGE", Website = w2 }
//                },

//Bill b = new Bill()
//            {
//                AccountId = 1,
//                AccountNum = "777777",
//                BillFrequency = BillFrequency.Monthly,
//                BillKind = BillKind.Reoccurring,
//                CommenceDate = DateTime.Today,
//                DayDueInMonth = 12,
//                Description = "Test bill",
//                IsActive = true,
//                Name = "Test bill",
//                PhoneNum = "503 277-9483",                
//                AutopayIsEnrolled = false
//            };

//            IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();
//            Bill persisted = billRepository.Add(b);