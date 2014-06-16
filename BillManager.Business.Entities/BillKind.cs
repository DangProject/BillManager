using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract(Name = "BillKind")]
    public enum BillKind
    {
        [EnumMember]
        Reoccurring = 1,
        [EnumMember]        
        Paydown = 2
    }
}
