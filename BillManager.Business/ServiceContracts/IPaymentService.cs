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
    public interface IPaymentService
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
    }
}
