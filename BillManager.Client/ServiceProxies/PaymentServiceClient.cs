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
    public class PaymentServiceClient : ServiceClientBase<IPaymentService>, IPaymentService
    {
        public Payment MakePayment(Payment payment)
        {
            return Channel.MakePayment(payment);
        }

        public IEnumerable<Payment> GetAllPaymentsByBill(int billId)
        {
            return Channel.GetAllPaymentsByBill(billId);
        }

        public IEnumerable<Payment> GetPaymentsByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            return Channel.GetPaymentsByBill(fromDate, toDate, billId);
        }

        public IEnumerable<Payment> GetPaymentsByBillForMonth(DateTime month, int billId)
        {
            return Channel.GetPaymentsByBillForMonth(month, billId);
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonth(DateTime month, int accountId)
        {
            return Channel.GetAllPaymentsByAccountForMonth(month, accountId);
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonths(DateTime fromMonth, DateTime toMonth, int accountId)
        {
            return Channel.GetAllPaymentsByAccountForMonths(fromMonth, toMonth, accountId);
        }

        public Payment GetCurrentMonthPaymentByBill(int billId)
        {
            return Channel.GetCurrentMonthPaymentByBill(billId);
        }

        public IEnumerable<Payment> GetCurrentMonthPaymentsByAccount(int accountId)
        {
            return Channel.GetCurrentMonthPaymentsByAccount(accountId);
        }

        public int GetTotalPaymentCountByBill(int billId)
        {
            return Channel.GetTotalPaymentCountByBill(billId);
        }

        public int GetPaymentCountByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            return Channel.GetPaymentCountByBill(fromDate, toDate, billId);
        }

        public decimal? GetRemainingBalanceByBill(int billId)
        {
            return Channel.GetRemainingBalanceByBill(billId);
        }


        # region Async Operations

        public Task<Payment> MakePaymentAsync(Payment payment)
        {
            return Channel.MakePaymentAsync(payment);
        }

        public Task<IEnumerable<Payment>> GetAllPaymentsByBillAsync(int billId)
        {
            return Channel.GetAllPaymentsByBillAsync(billId);
        }

        public Task<IEnumerable<Payment>> GetPaymentsByBillAsync(DateTime fromDate, DateTime toDate, int billId)
        {
            return Channel.GetPaymentsByBillAsync(fromDate, toDate, billId);
        }

        public Task<IEnumerable<Payment>> GetPaymentsByBillForMonthAsync(DateTime month, int billId)
        {
            return Channel.GetPaymentsByBillForMonthAsync(month, billId);
        }

        public Task<IEnumerable<Payment>> GetAllPaymentsByAccountForMonthAsync(DateTime month, int accountId)
        {
            return Channel.GetAllPaymentsByAccountForMonthAsync(month, accountId);
        }

        public Task<IEnumerable<Payment>> GetAllPaymentsByAccountForMonthsAsync(DateTime fromMonth, DateTime toMonth, int accountId)
        {
            return Channel.GetAllPaymentsByAccountForMonthsAsync(fromMonth, toMonth, accountId);
        }

        public Task<Payment> GetCurrentMonthPaymentByBillAsync(int billId)
        {
            return Channel.GetCurrentMonthPaymentByBillAsync(billId);
        }

        public Task<IEnumerable<Payment>> GetCurrentMonthPaymentsByAccountAsync(int accountId)
        {
            return Channel.GetCurrentMonthPaymentsByAccountAsync(accountId);
        }

        public Task<int> GetTotalPaymentCountByBillAsync(int billId)
        {
            return Channel.GetTotalPaymentCountByBillAsync(billId);
        }

        public Task<int> GetPaymentCountByBillAsync(DateTime fromDate, DateTime toDate, int billId)
        {
            return Channel.GetPaymentCountByBillAsync(fromDate, toDate, billId);
        }

        public Task<decimal?> GetRemainingBalanceByBillAsync(int billId)
        {
            return Channel.GetRemainingBalanceByBillAsync(billId);
        }

        # endregion
    }
}
