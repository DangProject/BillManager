using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
//using BillManager.Client.Model;
using Core.ServiceModel.Contracts;

namespace BillManager.Client.ServiceContracts
{
    [ServiceContract]
    public interface IAccountService : IServiceContract
    {
        [OperationContract]
        Account GetAccount(string userName, string password);

        [OperationContract]
        Account UpdateAccount(Account account);

        [OperationContract]
        Account CreateAccount(Account account);

        [OperationContract]
        IEnumerable<Bill> GetAllBills(int accountId);

        [OperationContract]
        IEnumerable<Bill> GetAllActiveBills(int accountId);

        [OperationContract]
        Bill AddNewBill(Bill bill);

        [OperationContract]
        Bill UpdateBill(Bill bill);

        [OperationContract]
        IEnumerable<Website> GetAllPaymentSites(int accountId);

        [OperationContract]
        IEnumerable<Category> GetAllBillCategories(int accountId);

        [OperationContract]
        Category AddNewCategory(Category category);

        [OperationContract]
        Website AddNewWebsite(Website website);

        #region Async operations

        [OperationContract]
        Task<Account> GetAccountAsync(string userName, string password);

        [OperationContract]
        Task<Account> UpdateAccountAsync(Account account);

        [OperationContract]
        Task<Account> CreateAccountAsync(Account account);

        [OperationContract]
        Task<IEnumerable<Bill>> GetAllBillsAsync(int accountId);

        [OperationContract]
        Task<IEnumerable<Bill>> GetAllActiveBillsAsync(int accountId);

        [OperationContract]
        Task<Bill> AddNewBillAsync(Bill bill);

        [OperationContract]
        Task<Bill> UpdateBillAsync(Bill bill);

        [OperationContract]
        Task<IEnumerable<Website>> GetAllPaymentSitesAsync(int accountId);

        [OperationContract]
        Task<IEnumerable<Category>> GetAllBillCategoriesAsync(int accountId);

        [OperationContract]
        Task<Category> AddNewCategoryAsync(Category category);

        [OperationContract]
        Task<Website> AddNewWebsiteAsync(Website website);

        #endregion
    }
}
