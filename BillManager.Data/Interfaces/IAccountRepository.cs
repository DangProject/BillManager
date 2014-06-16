using BillManager.Business.Entities;
using Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Data.Interfaces
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByUserNameAndPassword(string userName, string password);
    }
}
