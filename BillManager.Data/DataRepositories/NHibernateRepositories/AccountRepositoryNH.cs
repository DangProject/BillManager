using BillManager.Business.Entities;
using BillManager.Data.Interfaces;
using Core.Data.AbstractRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Data.DataRepositories.NHibernateRepositories
{
    public class AccountRepositoryNH : NHibernateRepository<Account>, IAccountRepository
    {
        public Account GetByUserNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
