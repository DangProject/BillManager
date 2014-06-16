using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillManager.Business.Entities;
using Core.Data.Interfaces;

namespace BillManager.Data.Interfaces
{
    public interface IFavoriteLinkRepository : IDataRepository<FavoriteLink>
    {
        IEnumerable<FavoriteLink> GetAllFavoriteSites(int accountId);
    }
}
