using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using Core.Data.Interfaces;

namespace BillManager.Data.Interfaces
{
    public interface IWebsiteRepository : IDataRepository<Website>
    {
        IEnumerable<Website> GetAllPaymentSites(int accountId);
    }
}
