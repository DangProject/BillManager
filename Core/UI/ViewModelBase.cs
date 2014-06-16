using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation.Results;

namespace Core.UI
{
    public class ViewModelBase : NotificationObject, IDataErrorInfo
    {
        public ViewModelBase()
        {
            InitializeICommands();
        }
        protected virtual void InitializeICommands() { }

        # region Service Client

        protected void UsingServiceClient<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            IDisposable disposibleClient = proxy as IDisposable;
            if (disposibleClient != null)
                disposibleClient.Dispose();
        }

        protected R UsingServiceClient<T, R>(T proxy, Func<T, R> codeToExecute)
        {
            R result = codeToExecute.Invoke(proxy);

            IDisposable disposibleClient = proxy as IDisposable;
            if (disposibleClient != null)
                disposibleClient.Dispose();

            return result;
        }

        protected void FaultHandledOperation(Action codeToExecute)
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        # endregion
        
        # region Model entity validation

        IEnumerable<ValidationFailure> validationErrors;
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return validationErrors; }
            set { validationErrors = value; }
        }        
        IList<ModelEntityBase> modelEntities;
        protected virtual void AddValidationEntities(IList<ModelEntityBase> modelEntities) { }
        protected void ValidateEntities()
        {
            if (modelEntities == null)
            {
                modelEntities = new List<ModelEntityBase>();
                AddValidationEntities(modelEntities);
            }

            if (modelEntities.Count > 0)
            {
                validationErrors = new List<ValidationFailure>();                
                foreach (ModelEntityBase m in modelEntities)
                {
                    if (m != null)
                    {
                        m.Validate();
                        validationErrors = validationErrors.Union(m.ValidationErrors).ToList();
                    }
                }

                foreach (ValidationFailure e in validationErrors)
                    FirePropertyChanged(e.PropertyName);
            }
        }
        protected void ValidateEntity(ModelEntityBase modelEntity)
        {
            if (modelEntity != null)
            {
                modelEntity.Validate();
                ValidationErrors = modelEntity.ValidationErrors;

                foreach (ValidationFailure e in validationErrors)
                    FirePropertyChanged(e.PropertyName);

                FirePropertyChanged("ValidationErrors");
            }
        }
        protected override void FirePropertyChanged(string property)
        {
            base.FirePropertyChanged(property);
            //ValidateEntities()
        }
        protected override void FirePropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {
            base.FirePropertyChanged<T>(propertyExpression);
            //ValidateEntities()
        }

        # endregion

        # region IDataErrorInfo

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                if (validationErrors != null && validationErrors.Count() > 0)
                {
                    foreach (ValidationFailure e in validationErrors)
                        if (e.PropertyName == columnName)
                            errors.AppendLine(e.ErrorMessage);
                }

                return errors.ToString();
            }
        }

        # endregion
    }
}
