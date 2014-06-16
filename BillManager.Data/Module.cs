using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Core.Data.Interfaces;

namespace BillManager.Data
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
            container.RegisterInstance<IDataRepositoryFactory>(container.Resolve<DataRepositoryFactoryEF>());
        }
    }
}
