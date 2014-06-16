using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract]
    public class Payment
    {
        [DataMember]
        public int PaymentId { get; set; }        
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public DateTime DatePaid { get; set; }
        [DataMember]                
        public DateTime PaymentMonthApplied { get; set; }
        [DataMember]
        public bool IsLate { get; set; }
        [DataMember]
        public string Comment { get; set; }        
        [DataMember]
        public int BillId { get; set; }
    }
}
