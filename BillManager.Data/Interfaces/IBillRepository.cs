using BillManager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Interfaces;

namespace BillManager.Data.Interfaces
{
    public interface IBillRepository : IDataRepository<Bill>
    {
        IEnumerable<Bill> GetAllBills(int accountId);
        IEnumerable<Bill> GetAllActiveBills(int accountId);
    }
}
