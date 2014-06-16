using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BillManager.Desktop
{
    public interface IPresentationController
    {
        void RequestCloseWindow(Type windowType);
        void RequestHideWindow(Type windowType);
        void RequestOpenWindow(Type windowType);        
        void RequestOpenWindowModal<T>(Type windowType, Action<T> windowSettings = default(Action<T>),
                                       RoutedEventHandler onWindowLoaded = default(RoutedEventHandler)) where T : Window;
        void RequestOpenWindowOneAtATime(Type windowType);
        void RequestNavigate(string regionName, string viewName);
        void RequestNavigate<T>(string regionName, string viewName, Action<T> viewModelSettings);
    }
}
