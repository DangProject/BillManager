using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Client.Model
{
    public class Payment
    {
        int paymentId;
        public int PaymentId
        {
            get { return paymentId; }
            set { paymentId = value; }
        }        
        decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        DateTime datePaid;
        public DateTime DatePaid
        {
            get { return datePaid; }
            set { datePaid = value; }
        }
        DateTime paymentMonthApplied;
        public DateTime PaymentMonthApplied
        {
            get { return paymentMonthApplied; }
            set { paymentMonthApplied = value; }
        }
        bool isLate;
        public bool IsLate
        {
            get { return isLate; }
            set { isLate = value; }
        }
        string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }        
        int billId;
        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }
    }
}
