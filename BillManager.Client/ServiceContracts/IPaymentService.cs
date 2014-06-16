using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
//using BillManager.Client.Model;
using Core.Exceptions;
using Core.ServiceModel.Contracts;

namespace BillManager.Client.ServiceContracts
{
    [ServiceContract]
    public interface IPaymentService : IServiceContract
    {
        [OperationContract]
        Payment MakePayment(Payment payment);

        [OperationContract]
        IEnumerable<Payment> GetAllPaymentsByBill(int billId);

        [OperationContract]
        IEnumerable<Payment> GetPaymentsByBill(DateTime fromDate, DateTime toDate, int billId);

        [OperationContract]
        IEnumerable<Payment> GetPaymentsByBillForMonth(DateTime month, int billId);

        [OperationContract]
        IEnumerable<Payment> GetAllPaymentsByAccountForMonth(DateTime month, int accountId);

        [OperationContract]
        IEnumerable<Payment> GetAllPaymentsByAccountForMonths(DateTime fromMonth, DateTime toMonth, int accountId);

        [OperationContract]
        Payment GetCurrentMonthPaymentByBill(int billId);

        [OperationContract]
        IEnumerable<Payment> GetCurrentMonthPaymentsByAccount(int accountId);

        [OperationContract]
        int GetTotalPaymentCountByBill(int billId);

        [OperationContract]
        int GetPaymentCountByBill(DateTime fromDate, DateTime toDate, int billId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(NotOfTypeException))]
        decimal? GetRemainingBalanceByBill(int billId);

        
        #region Async operations

        [OperationContract]
        Task<Payment> MakePaymentAsync(Payment payment);

        [OperationContract]
        Task<IEnumerable<Payment>> GetAllPaymentsByBillAsync(int billId);

        [OperationContract]
        Task<IEnumerable<Payment>> GetPaymentsByBillAsync(DateTime fromDate, DateTime toDate, int billId);

        [OperationContract]
        Task<IEnumerable<Payment>> GetPaymentsByBillForMonthAsync(DateTime month, int billId);

        [OperationContract]
        Task<IEnumerable<Payment>> GetAllPaymentsByAccountForMonthAsync(DateTime month, int accountId);

        [OperationContract]
        Task<IEnumerable<Payment>> GetAllPaymentsByAccountForMonthsAsync(DateTime fromMonth, DateTime toMonth, int accountId);

        [OperationContract]
        Task<Payment> GetCurrentMonthPaymentByBillAsync(int billId);

        [OperationContract]
        Task<IEnumerable<Payment>> GetCurrentMonthPaymentsByAccountAsync(int accountId);

        [OperationContract]
        Task<int> GetTotalPaymentCountByBillAsync(int billId);

        [OperationContract]
        Task<int> GetPaymentCountByBillAsync(DateTime fromDate, DateTime toDate, int billId);

        [OperationContract]
        Task<decimal?> GetRemainingBalanceByBillAsync(int billId);

        #endregion
    }
}
