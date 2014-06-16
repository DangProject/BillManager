using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using Core.Exceptions;

namespace BillManager.Business.ServiceContracts
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
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
    }
}
