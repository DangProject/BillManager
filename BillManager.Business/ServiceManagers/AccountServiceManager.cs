using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Business.ServiceContracts;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using Core.Exceptions;

namespace BillManager.Business.ServiceManagers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AccountServiceManager : ServiceManagerBase, IAccountService
    {
        public AccountServiceManager()
        {
        }
        [Import]
        IDataRepositoryFactory dataRepositoryFactory;
        public AccountServiceManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            this.dataRepositoryFactory = dataRepositoryFactory;
        }  
      
        public Account GetAccount(string userName, string password)
        {
            return FaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = dataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account account = accountRepository.GetByUserNameAndPassword(userName, password);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Invalid user name or password!", userName));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                else
                {
                    IFavoriteLinkRepository linkRepository = dataRepositoryFactory.GetDataRepository<IFavoriteLinkRepository>();
                    account.FavoriteLinks = new List<FavoriteLink>(linkRepository.GetAllFavoriteSites(account.AccountId));
                }

                return account;
            });
        }
        public Account UpdateAccount(Account account)
        {
            // not checking for null, because an account will always be in context
            return FaultHandledOperation(() =>
            {
                return dataRepositoryFactory.GetDataRepository<IAccountRepository>().Update(account);
            });
        }

        public Account CreateAccount(Account account)
        {
            return FaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                return accountRepository.Add(account);
            });
        }

        public IEnumerable<Bill> GetAllBills(int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();
                return billRepository.GetAllBills(accountId);
            });
        }

        public IEnumerable<Bill> GetAllActiveBills(int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();
                return billRepository.GetAllActiveBills(accountId);
            });
        }

        public Bill AddNewBill(Bill bill)
        {
            return FaultHandledOperation(() =>
            {
                IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();
                return billRepository.Add(bill);
            });
        }

        public Bill UpdateBill(Bill bill)
        {
            return FaultHandledOperation(() =>
            {
                // not checking for null, because an actual bill will be fetched first
                return dataRepositoryFactory.GetDataRepository<IBillRepository>().Update(bill);
            });
        }

        public IEnumerable<Website> GetAllPaymentSites(int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IWebsiteRepository websiteRepository = dataRepositoryFactory.GetDataRepository<IWebsiteRepository>();
                return websiteRepository.GetAllPaymentSites(accountId);
            });
        }

        public IEnumerable<Category> GetAllBillCategories(int accountId)
        {
            return FaultHandledOperation(() =>
            {
                ICategoryRepository categoryRepository = dataRepositoryFactory.GetDataRepository<ICategoryRepository>();
                return categoryRepository.GetAllCategoriesByAccount(accountId);
            });
        }

        public Category AddNewCategory(Category category)
        {
            return FaultHandledOperation(() =>
            {
                return dataRepositoryFactory.GetDataRepository<ICategoryRepository>().Add(category);
            });
        }

        public Website AddNewWebsite(Website website)
        {
            return FaultHandledOperation(() =>
            {
                return dataRepositoryFactory.GetDataRepository<IWebsiteRepository>().Add(website);
            });
        }
    }
}















//public IEnumerable<FavoriteLink> GetAllFavoriteSites(int accountId)
//{
//    return FaultHandledOperation(() =>
//    {
//        IFavoriteLinkRepository favoriteLinkRepository = dataRepositoryFactory.GetDataRepository<IFavoriteLinkRepository>();
//        return favoriteLinkRepository.GetAllFavoriteSites(accountId);
//    });
//}
