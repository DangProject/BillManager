using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
//using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using Core.ServiceModel;

namespace BillManager.Client.ServiceProxies
{
    public class AccountServiceClient : ServiceClientBase<IAccountService>, IAccountService
    {
        public Account GetAccount(string userName, string password)
        {
            return Channel.GetAccount(userName, password);
        }
        
        public Account UpdateAccount(Account account)
        {
            return Channel.UpdateAccount(account);
        }

        public Account CreateAccount(Account account)
        {
            return Channel.CreateAccount(account);
        }

        public IEnumerable<Bill> GetAllBills(int accountId)
        {
            return Channel.GetAllBills(accountId);
        }

        public IEnumerable<Bill> GetAllActiveBills(int accountId)
        {
            return Channel.GetAllActiveBills(accountId);
        }

        public Bill AddNewBill(Bill bill)
        {
            return Channel.AddNewBill(bill);
        }

        public Bill UpdateBill(Bill bill)
        {
            return Channel.UpdateBill(bill);
        }

        public IEnumerable<Website> GetAllPaymentSites(int accountId)
        {
            return Channel.GetAllPaymentSites(accountId);
        }

        public IEnumerable<Category> GetAllBillCategories(int accountId)
        {
            return Channel.GetAllBillCategories(accountId);
        }

        public Category AddNewCategory(Category category)
        {
            return Channel.AddNewCategory(category);
        }

        public Website AddNewWebsite(Website website)
        {
            return Channel.AddNewWebsite(website);
        }

        #region Async operations

        public Task<Account> GetAccountAsync(string userName, string password)
        {
            return Channel.GetAccountAsync(userName, password);
        }

        public Task<Account> UpdateAccountAsync(Account account)
        {
            return Channel.UpdateAccountAsync(account);
        }

        public Task<Account> CreateAccountAsync(Account account)
        {
            return Channel.CreateAccountAsync(account);
        }

        public Task<IEnumerable<Bill>> GetAllBillsAsync(int accountId)
        {
            return Channel.GetAllBillsAsync(accountId);
        }

        public Task<IEnumerable<Bill>> GetAllActiveBillsAsync(int accountId)
        {
            return Channel.GetAllActiveBillsAsync(accountId);
        }
                
        public Task<Bill> AddNewBillAsync(Bill bill)
        {
            return Channel.AddNewBillAsync(bill);
        }
        
        public Task<Bill> UpdateBillAsync(Bill bill)
        {
            return Channel.UpdateBillAsync(bill);
        }
        
        public Task<IEnumerable<Website>> GetAllPaymentSitesAsync(int accountId)
        {
            return Channel.GetAllPaymentSitesAsync(accountId);
        }

        public Task<IEnumerable<Category>> GetAllBillCategoriesAsync(int accountId)
        {
            return Channel.GetAllBillCategoriesAsync(accountId);
        }

        public Task<Category> AddNewCategoryAsync(Category category)
        {
            return Channel.AddNewCategoryAsync(category);
        }

        public Task<Website> AddNewWebsiteAsync(Website website)
        {
            return Channel.AddNewWebsiteAsync(website);
        }

        #endregion
    }
}
