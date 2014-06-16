using BillManager.Business.Entities;
using BillManager.Data.AbstractRepositories;
using BillManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace BillManager.Data.DataRepositories.EntityFrameworkRepositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepositoryEF : EntityFrameworkRepository<Account>, IAccountRepository
    {
        protected override Account AddEntity(BillManagerContext context, Account entity)
        {
            return context.Accounts.Add(entity);
        }

        protected override Account GetEntity(BillManagerContext context, int id)
        {
            return context.Accounts.FirstOrDefault(a => a.AccountId == id);
        }

        protected override Account UpdateEntity(BillManagerContext context, Account entity)
        {
            return context.Accounts.FirstOrDefault(a => a.AccountId == entity.AccountId);
        }

        protected override IEnumerable<Account> GetEntities(BillManagerContext context)
        {
            return context.Accounts.AsEnumerable();
        }

        public Account GetByUserNameAndPassword(string userName, string password)
        {
            using (BillManagerContext context = new BillManagerContext())
                return context.Accounts.FirstOrDefault(a => a.UserName == userName && a.Password == password);
        }
    }
}
