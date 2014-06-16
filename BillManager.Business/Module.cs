using Core.Data.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Business
{
    public class Module : IModule
    {
        IUnityContainer container;
        public Module(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {            
        }
    }
}
