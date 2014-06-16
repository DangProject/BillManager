using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business.Entities
{
    [DataContract]
    public class FavoriteLink
    {
        [DataMember]
        public int FavoriteLinkId { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public Website Website { get; set; }
    }
}



