using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Data.AbstractRepositories;
using BillManager.Data.Interfaces;
using Core.Extensions;

namespace BillManager.Data.DataRepositories.EntityFrameworkRepositories
{
    [Export(typeof(IWebsiteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WebsiteRepositoryEF : EntityFrameworkRepository<Website>, IWebsiteRepository
    {
        protected override Website AddEntity(BillManagerContext context, Website entity)
        {
            return context.Websites.Add(entity);
        }

        protected override Website GetEntity(BillManagerContext context, int id)
        {
            return context.Websites.Find(id);
        }

        protected override Website UpdateEntity(BillManagerContext context, Website entity)
        {
            return context.Websites.FirstOrDefault(w => w.WebsiteId == entity.WebsiteId);
        }

        protected override IEnumerable<Website> GetEntities(BillManagerContext context)
        {
            return context.Websites.AsEnumerable();
        }

        public IEnumerable<Website> GetAllPaymentSites(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from w in context.Websites                        
                        where w.AccountId == accountId
                        select w).ToArray();
            };
        }
    }
}
