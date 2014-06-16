using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using Core.Data.Interfaces;

namespace BillManager.Data.Interfaces
{
    public interface ICategoryRepository : IDataRepository<Category>
    {
        IEnumerable<Category> GetAllCategoriesByAccount(int accountId);
    }
}
