using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Client.Model
{
    //[DataContract(Name = "BillFrequency")]
    public enum BillFrequency
    {
        /// <summary>
        /// Monthly
        /// </summary>
        //[EnumMember]
        Monthly = 1,
        /// <summary>
        /// Every 2 months
        /// </summary>
        //[EnumMember]
        BiMonthly = 2,
        /// <summary>
        /// Twice a year
        /// </summary>
        //[EnumMember]
        SemiYearly = 3,
        /// <summary>
        /// Yearly
        /// </summary>
        //[EnumMember]
        Yearly = 4
    }
}
