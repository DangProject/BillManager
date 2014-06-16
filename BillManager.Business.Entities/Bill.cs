
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract(IsReference = true)]
    public class Bill
    {
        [DataMember]
        public int BillId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string AccountNum { get; set; }
        [DataMember]
        public DateTime CommenceDate { get; set; }
        [DataMember]
        public BillKind BillKind { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public BillFrequency BillFrequency { get; set; }
        [DataMember]
        public int DayDueInMonth { get; set; }
        [DataMember]
        public bool AutopayIsEnrolled { get; set; }
        [DataMember]
        public decimal? InitialBalance { get; set; }        
        [DataMember]
        public string PhoneNum { get; set; }        
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public Category Category { get; set; }
        [DataMember]
        public ICollection<PayOption> PayOptions { get; set; }
    }
}
