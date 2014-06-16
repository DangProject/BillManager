using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public ICollection<Bill> Bills { get; set; }
    }
}
