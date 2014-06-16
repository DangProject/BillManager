using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Table
            this.ToTable("Accounts");

            // Primary Key
            this.HasKey(t => t.AccountId);
            //this.HasKey(t => new { t.AccountId, t.UserName });

            // Properties required
            this.Property(t => t.FirstName).IsRequired().HasMaxLength(20);
            this.Property(t => t.LastName).IsRequired().HasMaxLength(20);
            this.Property(t => t.UserName).IsRequired().HasMaxLength(20);
            this.Property(t => t.Password).IsRequired().HasMaxLength(20);
            // Properties optional
            //this.Property(t => t.Email);
        }
    }
}
