using BillManager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Interfaces;

namespace BillManager.Data.Interfaces
{
    public interface IPaymentRepository : IDataRepository<Payment>
    {
        IEnumerable<Payment> GetAllPaymentsByBill(int billId);
        IEnumerable<Payment> GetPaymentsByBill(DateTime fromDate, DateTime toDate, int billId);
        IEnumerable<Payment> GetPaymentsByBillForMonth(DateTime month, int billId);        
        IEnumerable<Payment> GetAllPaymentsByAccountForMonth(DateTime month, int accountId);
        IEnumerable<Payment> GetAllPaymentsByAccountForMonths(DateTime fromMonth, DateTime toMonth, int accountId);
        Payment GetCurrentMonthPaymentByBill(int billId);
        IEnumerable<Payment> GetCurrentMonthPaymentsByAccount(int accountId);
        int GetTotalPaymentCountByBill(int billId);
        int GetPaymentCountByBill(DateTime fromDate, DateTime toDate, int billId);
    }
}
