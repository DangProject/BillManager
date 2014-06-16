using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            // Table
            this.ToTable("Payments");

            // Primary Key
            this.HasKey(t => t.PaymentId);

            // Properties required
            this.Property(t => t.Amount).IsRequired();
            this.Property(t => t.DatePaid).IsRequired();
            this.Property(t => t.IsLate).IsRequired();
            // Properties optional
            this.Property(t => t.Comment);
        }
    }
}
