﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Client.Enums
{
    public enum BillStatus
    {
        Late,
        NotPaid,        
        Paid,
        NoBillThisMonth,
        CantStart
    }
}