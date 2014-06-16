﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using BillManager.Client.Model;

namespace BillManager.Desktop
{
    public class BillLoadedEvent : CompositePresentationEvent<ICollection<Bill>>
    {
    }
}