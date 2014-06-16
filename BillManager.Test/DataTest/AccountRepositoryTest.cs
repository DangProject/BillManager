using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using BillManager.Data;
using Core.MEF;
using BillManager.Business;
using System.ComponentModel.Composition;

namespace BillManager.Test
{
    [TestFixture]
    public class AccountRepositoryTest
    {
        //IUnityContainer container;
        [Import]
        IDataRepositoryFactory repositoryFactory;
        IAccountRepository accountRepository;
        [TestFixtureSetUp]
        public void Setup()
        {
            //this.container = new UnityContainer();            
            //container.RegisterInstance<IDataRepositoryFactory>(container.Resolve<DataRepositoryFactoryEF>());
            //repositoryFactory = container.Resolve<IDataRepositoryFactory>();

            MEF.Container = new Bootstapper().InitializeContainer();
            if (MEF.Container != null)
                MEF.Container.SatisfyImportsOnce(this);
            
            accountRepository = repositoryFactory.GetDataRepository<IAccountRepository>();
        }        
        [Test]
        public void CanAddRecord()
        {            
            Account account = new Account()
            {
                FirstName = "Dang",
                LastName = "Nguyen",
                UserName = "DangNguyen",
                Password = "dangfinance"
            };

            Assert.IsTrue(account.AccountId == default(int));

            Account addedAccount = accountRepository.Add(account);

            Assert.IsTrue(addedAccount.AccountId != default(int));
        }
        [Test]
        public void CanUpdateRecord()
        {
            Account account = accountRepository.GetByUserNameAndPassword("DevinThomas", "devinthomas");
            //Account account = accountRepository.Get(1);
            Account updatedAccount = null;

            if (account != null)
            {
                account.FirstName = "Devin";
                account.LastName = "Jones";
                account.UserName = "DevinJones";
                account.Password = "devinjones";
                account.Email = "djones@gmail.com";
                updatedAccount = accountRepository.Update(account);
            }

            Assert.IsNotNull(updatedAccount);
            Assert.IsTrue(updatedAccount.Email == account.Email);
        }
        [Test]
        public void CanGetRecord()
        {   
            Account account = accountRepository.Get(1);
            Assert.IsNotNull(account);
        }
        [Test]
        public void CanDeleteRecord()
        {
            accountRepository.Remove(2);
            Assert.IsTrue(accountRepository.Get().Count() == 2);
        }
        [Test]
        public void CannotAddUniqueRecord()
        {
            Account account = new Account()
            {
                FirstName = "Brandon",
                LastName = "Baker",
                UserName = "bb",
                Password = "bb",
                Email = "bb@gmail.com"
            };
        }
    }
}

