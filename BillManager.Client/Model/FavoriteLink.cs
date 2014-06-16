using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Client.Model
{
    public class FavoriteLink
    {
        int favoriteLinkId;
        public int FavoriteLinkId
        {
            get { return favoriteLinkId; }
            set { favoriteLinkId = value; }
        }
        int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }        
        string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }
        Website website;
        public Website Website
        {
            get { return website; }
            set { website = value; }
        }
    }
}
