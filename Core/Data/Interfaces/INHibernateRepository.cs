using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Interfaces
{
    public interface INHibernateRepository<T> : IDataRepository<T>
        where T : class, new()
    {
        IEnumerable<T> GetByCriteria(params ICriterion[] criterion);
        void Commit();
    }
}
