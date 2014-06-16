using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Core.UI
{
    public class ModelEntityBase : NotificationObject
    {
        public ModelEntityBase()
        {
            validator = GetValidator();
        }

        # region IValidator

        protected IValidator validator = null;
        protected virtual IValidator GetValidator()
        {
            return null;
        }

        protected IEnumerable<ValidationFailure> validationErrors;
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return validationErrors; }
        }
        
        public void Validate()
        {
            if (validator != null)
            {
                ValidationResult results = validator.Validate(this);
                validationErrors = results.Errors;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                if (validationErrors != null && validationErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }

        # endregion
    }
}
