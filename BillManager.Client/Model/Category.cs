using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.UI;
using FluentValidation;

namespace BillManager.Client.Model
{
    public class Category : ModelEntityBase
    {
        public Category()
        {
            //bills = new List<Bill>();
        }
        int categoryId;
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }
        ICollection<Bill> bills;
        public ICollection<Bill> Bills
        {
            get { return bills; }
            set { bills = value; }
        }

        protected override IValidator GetValidator()
        {
            return new CategoryValidator();
        }
        class CategoryValidator : AbstractValidator<Category>
        {
            public CategoryValidator()
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required");
            }
        }
    }
}
