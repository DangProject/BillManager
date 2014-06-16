//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFModelFirst1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public Bill()
        {
            this.PayOptions = new HashSet<PayOption>();
        }
    
        public int BillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CommenceDate { get; set; }
        public int BillKind { get; set; }
        public bool IsActive { get; set; }
        public int BillFrequency { get; set; }
        public int DayDueInMonth { get; set; }
        public Nullable<bool> AutopayIsEnrolled { get; set; }
        public string PhoneNum { get; set; }
        public Nullable<decimal> InitialBalance { get; set; }
        public string AccountNum { get; set; }
        public int AccountId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<PayOption> PayOptions { get; set; }
    }
}
