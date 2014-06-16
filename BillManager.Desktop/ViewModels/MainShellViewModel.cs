using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Model;
using Core.UI;
using Microsoft.Practices.Prism.Events;

namespace BillManager.Desktop.ViewModels
{
    public class MainShellViewModel : ViewModelBase
    {
        public MainShellViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<BillSelectionChangedEvent>().Subscribe((b) => TransitionObject = b, true);
        }        
        object transitionObject;
        public object TransitionObject
        {
            get { return transitionObject; }
            set 
            {
                if (transitionObject != value)
                {
                    transitionObject = value;
                    FirePropertyChanged(() => TransitionObject);
                }
            }
        }
    }
}
