using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BillManager.Desktop.Views;
using Core.UI;
using Microsoft.Practices.Prism.Commands;

namespace BillManager.Desktop
{
    public class ToolsViewModel : ViewModelBase
    {
        IPresentationController controller;
        public ToolsViewModel(IPresentationController controller)
        {
            this.controller = controller;
        }
        protected override void InitializeICommands()
        {
            NavigateToAddBillCommand = new DelegateCommand<object>(OnNavigateToAddBillCommand);
        }
        public ICommand NavigateToAddBillCommand { get; set; }
        void OnNavigateToAddBillCommand(object o)
        {
            //controller.RequestOpenWindowModal(typeof(AddBillWindow));     
        }
    }
}
