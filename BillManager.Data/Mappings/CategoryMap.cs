using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Data.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Table
            this.ToTable("Categories");

            // Primary Key
            this.HasKey(t => t.CategoryId);
            
            // Properties
            this.Property(t => t.Name).IsRequired().HasMaxLength(20);
            //this.Property(t => t.AccountId)

            // Relationships
            this.HasMany(t => t.Bills).WithOptional(t => t.Category);
        }
    }
}
