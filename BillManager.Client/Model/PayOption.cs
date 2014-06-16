using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.UI;

namespace BillManager.Client.Model
{
    public class PayOption : ModelEntityBase
    {
        public PayOption() {}
        public PayOption(Website website, Bill bill)
        {
            this.website = website;
            this.bill = bill;
        }
        int payOptionId;
        public int PayOptionId
        {
            get { return payOptionId; }
            set { payOptionId = value; }
        }
        string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }
        bool isPrimary;
        public bool IsPrimary
        {
            get { return isPrimary; }
            set 
            { 
                isPrimary = value;
                FirePropertyChanged("IsPrimary");
            }
        }
        Bill bill;
        public Bill Bill
        {
            get { return bill; }
            set { bill = value; }
        }
        Website website;
        public Website Website
        {
            get { return website; }
            set { website = value; }
        }
    }
}
