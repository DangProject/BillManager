using BillManager.Client.Model;
using Core.UI;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Desktop
{
    public class BillSummaryViewModel : ViewModelBase
    {
        public BillSummaryViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<BillSelectionChangedEvent>().Subscribe(OnBillSelectionChanged, true);
        }
        void OnBillSelectionChanged(Bill bill)
        {
            Bill = bill;
        }
        Bill bill;
        public Bill Bill
        {
            get { return bill; }
            set 
            { 
                bill = value;
                FirePropertyChanged("Bill");
            }
        }
    }
}
