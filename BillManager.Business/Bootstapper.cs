using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Data.DataRepositories.EntityFrameworkRepositories;
using Core.MEF;

namespace BillManager.Business
{
    public class Bootstapper : MEFBootstrapper
    {        
        protected override void ConfigureCatalog()
        {
            Catalogs.Add(new AssemblyCatalog(typeof(AccountRepositoryEF).Assembly));
        }        
    }
}
