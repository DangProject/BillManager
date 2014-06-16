using BillManager.Business.Entities;
using BillManager.Data.AbstractRepositories;
using BillManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using System.ComponentModel.Composition;
using System.Collections;
using System.Data.Entity;

namespace BillManager.Data.DataRepositories.EntityFrameworkRepositories
{
    [Export(typeof(IBillRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BillRepositoryEF : EntityFrameworkRepository<Bill>, IBillRepository
    {
        protected override Bill AddEntity(BillManagerContext context, Bill entity)
        {
            if (entity.Category != null)
                entity.Category = context.Categories.Find(entity.Category.CategoryId);

            foreach (PayOption p in entity.PayOptions)
                p.Website = context.Websites.Find(p.Website.WebsiteId);
           
            return context.Bills.Add(entity);
        }

        protected override Bill GetEntity(BillManagerContext context, int id)
        {
            return context.Bills.Find(id);
        }

        protected override Bill UpdateEntity(BillManagerContext context, Bill entity)
        {
            return context.Bills.FirstOrDefault(b => b.BillId == entity.BillId);
        }

        protected override IEnumerable<Bill> GetEntities(BillManagerContext context)
        {
            return context.Bills.AsEnumerable();
        }

        public IEnumerable<Bill> GetAllBills(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        select b).ToArray();
            }
        }

        public IEnumerable<Bill> GetAllActiveBills(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills.Include(b => b.Category).Include(b => b.PayOptions.Select(p => p.Website))
                        where b.AccountId == accountId && b.IsActive == true
                        select b).ToArray();
            }
        }
    }
}


//Website website1 = ((IList<PayOption>)entity.PayOptions)[0].Website;
//int hash1 = website1.GetHashCode();
//Website website2 = context.Websites.First(w => w.WebsiteId == website1.WebsiteId);
//int hash2 = website2.GetHashCode();
//Website website3 = context.Websites.Find(website1.WebsiteId);
//int hash3 = website3.GetHashCode();
//((IList<PayOption>)entity.PayOptions)[0].Website = website2;