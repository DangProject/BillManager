using Core.Data.AbstractRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Data.AbstractRepositories
{
    public abstract class EntityFrameworkRepository<T> : EntityFrameworkRepositoryBase<T, BillManagerContext>
        where T : class, new()
    {   
    }
}
