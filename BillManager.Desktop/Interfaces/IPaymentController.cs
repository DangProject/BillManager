using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Model;

namespace BillManager.Desktop.Interfaces
{
    public interface IPaymentController
    {
        IEnumerable<Bill> GetCurrentBills();
        void UpdateBill(Bill bill, Payment payment);
    }
}
