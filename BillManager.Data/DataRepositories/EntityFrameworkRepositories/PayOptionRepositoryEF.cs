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
    [Export(typeof(IPayOptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PayOptionRepositoryEF : EntityFrameworkRepository<PayOption>, IPayOptionRepository
    {
        protected override PayOption AddEntity(BillManagerContext context, PayOption entity)
        {  
            return context.PayOptions.Add(entity);
        }

        protected override PayOption GetEntity(BillManagerContext context, int id)
        {
            return context.PayOptions.Find(id);
        }

        protected override PayOption UpdateEntity(BillManagerContext context, PayOption entity)
        {
            return context.PayOptions.FirstOrDefault(p => p.PayOptionId == entity.PayOptionId);
        }

        protected override IEnumerable<PayOption> GetEntities(BillManagerContext context)
        {
            return context.PayOptions.AsEnumerable();
        }
    }
}
