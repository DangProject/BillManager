using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Expression.Interactivity;

namespace BillManager.Desktop
{
    public class BindVisualStateBehavior : Behavior<FrameworkElement>
    {           
        public static readonly DependencyProperty StateNameProperty =
            DependencyProperty.Register("StateName", typeof(string), typeof(BindVisualStateBehavior), new PropertyMetadata(StateNamePropertyChanged));
        
        public string StateName
        {
            get { return (string)GetValue(StateNameProperty); }
            set { SetValue(StateNameProperty, value); }
        }

        static void StateNamePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs ars)
        {
            ((BindVisualStateBehavior)obj).UpdateVisualState(
                (string)ars.NewValue,
                useTransitions: true);
        }

        protected override void OnAttached()
        {
            UpdateVisualState(StateName, useTransitions: false);
            base.OnAttached();
        }
        private void UpdateVisualState(string visualState, bool useTransitions)
        {
            if (AssociatedObject != null)
            {                
                FrameworkElement stateTarget;
                if (VisualStateUtilities.TryFindNearestStatefulControl(AssociatedObject, out stateTarget))
                    VisualStateUtilities.GoToState(stateTarget, visualState, useTransitions);
            }
        }
    }
}
