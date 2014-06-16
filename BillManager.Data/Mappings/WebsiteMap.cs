using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class WebsiteMap : EntityTypeConfiguration<Website>
    {
        public WebsiteMap()
        {
            // Table
            this.ToTable("Websites");

            // Primary Key
            this.HasKey(t => t.WebsiteId);

            // Properties required
            this.Property(t => t.Name).IsRequired().HasMaxLength(20);
            this.Property(t => t.UrlString).IsRequired();
            this.Property(t => t.AccountId).IsRequired();
            // Properties optional
            this.Property(t => t.Description);
            this.Property(t => t.UserName);
            this.Property(t => t.Password);
        }
    }
}
