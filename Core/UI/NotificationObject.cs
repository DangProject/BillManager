using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.UI
{
    public class NotificationObject : INotifyPropertyChanged
    {
        protected List<PropertyChangedEventHandler> propertyChangedSubscribers = new List<PropertyChangedEventHandler>();

        event PropertyChangedEventHandler propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (!propertyChangedSubscribers.Contains(value))
                {
                    propertyChanged += value;
                    propertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                propertyChanged -= value;
                propertyChangedSubscribers.Remove(value);
            }
        }

        protected virtual void FirePropertyChanged(string property)
        {
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void FirePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            FirePropertyChanged(body.Member.Name);
        }  
    }
}
