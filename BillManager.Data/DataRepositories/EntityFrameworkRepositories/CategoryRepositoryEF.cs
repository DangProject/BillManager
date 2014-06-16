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
    [Export(typeof(ICategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryRepositoryEF : EntityFrameworkRepository<Category>, ICategoryRepository
    {
        protected override Category AddEntity(BillManagerContext context, Category entity)
        {
            return context.Categories.Add(entity);
        }

        protected override Category GetEntity(BillManagerContext context, int id)
        {
            return context.Categories.Find(id);
        }

        protected override Category UpdateEntity(BillManagerContext context, Category entity)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == entity.CategoryId);
        }

        protected override IEnumerable<Category> GetEntities(BillManagerContext context)
        {
            return context.Categories.AsEnumerable();
        }

        public IEnumerable<Category> GetAllCategoriesByAccount(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from c in context.Categories                        
                        where c.AccountId == accountId
                        select c).ToArray();

                //join a in context.Accounts on c.AccountId equals a.AccountId
            };
        }
    }
}
