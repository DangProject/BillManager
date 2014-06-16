using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Core.Extensions
{
    public static class DependencyObjectExtensions
    {
        //public static DependencyObject FindVisualChild(this DependencyObject reference, string childName, Type childType)
        //{
        //    DependencyObject foundChild = null;
        //    if (reference != null)
        //    {
        //        int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
        //        for (int i = 0; i < childrenCount; i++)
        //        {
        //            var child = VisualTreeHelper.GetChild(reference, i);
        //            // If the child is not of the request child type child
        //            if (child.GetType() != childType)
        //            {
        //                // recursively drill down the tree
        //                foundChild = FindVisualChild(child, childName, childType);
        //            }
        //            else if (!string.IsNullOrEmpty(childName))
        //            {
        //                var frameworkElement = child as FrameworkElement;
        //                // If the child's name is set for search
        //                if (frameworkElement != null && frameworkElement.Name == childName)
        //                {
        //                    // if the child's name is of the request name
        //                    foundChild = child;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                // child element found.
        //                foundChild = child;
        //                break;
        //            }
        //        }
        //    }
        //    return foundChild;
        //}

        public static DependencyObject FindLogicalChild(this DependencyObject reference, Func<DependencyObject, bool> matchPattern)
        {
            DependencyObject[] children = LogicalTreeHelper.GetChildren(reference).OfType<DependencyObject>().ToArray();
            DependencyObject foundChild = null;
            int i = 0;
            while (foundChild == null && i < children.Length)
            {
                if (matchPattern(children[i]))
                    foundChild = children[i];

                i++;
            }

            i = 0;
            while (foundChild == null && i < children.Length)
            {
                foundChild = FindVisualChild(children[i], matchPattern);

                i++;
            }

            return foundChild;
        }

        public static DependencyObject FindVisualChild(this DependencyObject reference, Func<DependencyObject, bool> matchPattern)
        {
            DependencyObject[] children = new DependencyObject[VisualTreeHelper.GetChildrenCount(reference)];
            DependencyObject foundChild = null;
            int i = 0;
            while (foundChild == null && i < children.Length)
            {
                children[i] = VisualTreeHelper.GetChild(reference, i);

                if (matchPattern(children[i]))
                    foundChild = children[i];

                i++;
            }

            i = 0;
            while (foundChild == null && i < children.Length)
            {
                foundChild = FindVisualChild(children[i], matchPattern);

                i++;
            }

            return foundChild;
        }
    }
}
