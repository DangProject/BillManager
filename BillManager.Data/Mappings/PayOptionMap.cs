using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class PayOptionMap : EntityTypeConfiguration<PayOption>
    {
        public PayOptionMap()
        {
            // Table
            this.ToTable("PayOptions");

            // Primary Key
            this.HasKey(t => t.PayOptionId);

            // Properties
            this.Property(t => t.Label).IsRequired().HasMaxLength(20);
            this.Property(t => t.IsPrimary).IsRequired();            
            //this.Property(t => t.BillId)            

            // Relationships
            this.HasRequired(t => t.Bill);
            this.HasRequired(p => p.Website);
        }
    }
}
