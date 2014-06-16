using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using Core.Extensions;
using System.Windows.Controls;

namespace BillManager.Desktop
{
    public class AttachHyperlinkItemBehavior : Behavior<FrameworkElement>
    {
        //public static readonly DependencyProperty StateNameProperty =
        //    DependencyProperty.Register("StateName", typeof(string), typeof(AttachHyperlinkItemBehavior), 
        //    new PropertyMetadata(StateNamePropertyChanged));

        //public string StateName
        //{
        //    get { return (string)GetValue(StateNameProperty); }
        //    set { SetValue(StateNameProperty, value); }
        //}

        //static void StateNamePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs ars)
        //{
        //    ((BindVisualStateBehavior)obj).UpdateVisualState(
        //        (string)ars.NewValue,
        //        useTransitions: true);
        //}

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += (s, e) =>
            {
                DependencyObject viewer = AssociatedObject.FindLogicalChild(d => d is ScrollViewer);
            };
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        //    UpdateVisualState(StateName, useTransitions: false);
        //    base.OnAttached();
        //}
        //private void UpdateVisualState(string visualState, bool useTransitions)
        //{
        //    if (AssociatedObject != null)
        //    {                
        //        FrameworkElement stateTarget;
        //        if (VisualStateUtilities.TryFindNearestStatefulControl(AssociatedObject, out stateTarget))
        //            VisualStateUtilities.GoToState(stateTarget, visualState, useTransitions);
        //    }
        //}
    }
}

