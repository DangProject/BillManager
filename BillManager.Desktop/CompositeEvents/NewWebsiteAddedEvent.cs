using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Model;
using Microsoft.Practices.Prism.Events;

namespace BillManager.Desktop
{
    public class NewWebsiteAddedEvent : CompositePresentationEvent<Website>
    {
    }
}
