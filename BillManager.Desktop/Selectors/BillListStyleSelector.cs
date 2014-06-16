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
    public class BillListStyleSelector : StyleSelector
    {
        public Style LateStyle { get; set; }        
        public Style NotPaidStyle { get; set; }        
        public Style PaidStyle { get; set; }
        public Style NoBillThisMonthStyle { get; set; }
        public Style CantStartStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Bill bill = item as Bill;

            switch (bill.BillStatus)
            {
                case BillStatus.Late:
                    return LateStyle;
                case BillStatus.NotPaid:
                    return NotPaidStyle;                
                case BillStatus.Paid:
                    return PaidStyle;
                case BillStatus.NoBillThisMonth:
                    return NoBillThisMonthStyle;
                case BillStatus.CantStart:
                    return CantStartStyle;
            }

            return CantStartStyle;
        }
    }
}
