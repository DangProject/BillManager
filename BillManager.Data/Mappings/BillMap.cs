using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class BillMap : EntityTypeConfiguration<Bill>
    {
        public BillMap()
        {
            // Table
            this.ToTable("Bills");

            // Primary Key
            this.HasKey(t => t.BillId);

            // Properties required
            this.Property(t => t.Name).IsRequired().HasMaxLength(20);            
            this.Property(t => t.BillKind).IsRequired();
            this.Property(t => t.IsActive).IsRequired();
            this.Property(t => t.BillFrequency).IsRequired();
            this.Property(t => t.DayDueInMonth).IsRequired();
            // Properties optional
            this.Property(t => t.PhoneNum).HasMaxLength(20); 
            //this.Property(t => t.Description);
            //this.Property(t => t.AccountNum);
            //this.Property(t => t.CommenceDate);            
            //this.Property(t => t.AutopayIsEnrolled);
            //this.Property(t => t.InitialBalance);            
            //this.Property(t => t.AccountId);
            //this.Property(t => t.Category_CategoryId);

            // Relationships
            this.HasOptional(t => t.Category).WithMany(t => t.Bills);
            //this.HasMany(b => b.PayOptions).WithRequired(p => p.Bill).WillCascadeOnDelete(true);
        }
    }
}
