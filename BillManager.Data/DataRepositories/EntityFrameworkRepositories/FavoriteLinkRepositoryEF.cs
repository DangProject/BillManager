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
    [Export(typeof(IFavoriteLinkRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FavoriteLinkRepositoryEF : EntityFrameworkRepository<FavoriteLink>, IFavoriteLinkRepository
    {
        protected override FavoriteLink AddEntity(BillManagerContext context, FavoriteLink entity)
        {
            //int hash1 = entity.Website.GetHashCode();
            //Website w1 = context.Websites.First();
            //int hashc = w1.GetHashCode();
            //int hash2 = entity.Website.GetHashCode();
            
            entity.Website = context.Websites.First(w => w.WebsiteId == entity.Website.WebsiteId);            
            return context.FavoriteLinks.Add(entity);
        }

        protected override FavoriteLink GetEntity(BillManagerContext context, int id)
        {
            return context.FavoriteLinks.Find(id);
        }

        protected override FavoriteLink UpdateEntity(BillManagerContext context, FavoriteLink entity)
        {
            return context.FavoriteLinks.FirstOrDefault(f => f.FavoriteLinkId == entity.FavoriteLinkId);
        }

        protected override IEnumerable<FavoriteLink> GetEntities(BillManagerContext context)
        {
            return context.FavoriteLinks.AsEnumerable();
        }

        public IEnumerable<FavoriteLink> GetAllFavoriteSites(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from f in context.FavoriteLinks
                        join a in context.Accounts on f.AccountId equals a.AccountId
                        select f).ToArray();
                //return (from f in context.FavoriteLinks
                //        join a in context.Accounts on f.AccountId equals a.AccountId
                //        select f.Website).Distinct().ToArray();
            };
        }
    }
}
