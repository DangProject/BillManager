using BillManager.Client.Model;
using Core.UI;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillManager.Desktop
{
    public class NoBillSelectedViewModel : ViewModelBase
    {
        public NoBillSelectedViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<BillLoadedEvent>().Subscribe(c => Bills = c, true);
        }
        ICollection<Bill> bills;
        public ICollection<Bill> Bills
        {
            get { return bills; }
            set 
            { 
                bills = value;
                FirePropertyChanged("Bills");
            }
        }
    }
}
