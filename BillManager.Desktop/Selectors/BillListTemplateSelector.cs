using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BillManager.Client.Enums;
using BillManager.Client.Model;

namespace BillManager.Desktop
{
    public class BillListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LateTemplate { get; set; }
        public DataTemplate NotPaidTemplate { get; set; }        
        public DataTemplate PaidTemplate { get; set; }
        public DataTemplate NoBillThisMonthTemplate { get; set; }
        public DataTemplate CantStartTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Bill bill = item as Bill;

            switch (bill.BillStatus)
            {
                case BillStatus.Late:
                    return LateTemplate;                
                case BillStatus.NotPaid:
                    return NotPaidTemplate;                
                case BillStatus.Paid:
                    return PaidTemplate;
                case BillStatus.NoBillThisMonth:
                    return NoBillThisMonthTemplate;
                case BillStatus.CantStart:
                    return CantStartTemplate;
            }

            return CantStartTemplate;
        }
    }
}
