using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class FavoriteLinkMap : EntityTypeConfiguration<FavoriteLink>
    {
        public FavoriteLinkMap()
        {
            // Table
            this.ToTable("FavoriteLinks");

            // Primary Key
            this.HasKey(t => t.FavoriteLinkId);

            // Properties
            this.Property(t => t.Label).IsRequired().HasMaxLength(20);
            this.Property(t => t.AccountId).IsRequired();
            //this.Property(t => t.Website_WebsiteId).IsRequired();
            

            // Relationships
            this.HasRequired(f => f.Website); 
        }
    }
}
