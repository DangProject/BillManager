using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract]
    public class PayOption
    {
        [DataMember]
        public int PayOptionId { get; set; }        
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public bool IsPrimary { get; set; }        
        [DataMember]
        public Website Website { get; set; }        
        [DataMember]
        public Bill Bill { get; set; }
    }
}
