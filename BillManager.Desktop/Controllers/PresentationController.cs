using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BillManager.Desktop.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace BillManager.Desktop
{
    public class PresentationController : IPresentationController
    {
        IUnityContainer container;
        IRegionManager regionManager;
        IDictionary<Type, Window> windows;
        public PresentationController(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            windows = new Dictionary<Type, Window>();
        }
        public void RequestOpenWindowModal<T>(Type windowType, Action<T> windowSettings = default(Action<T>),
                                              RoutedEventHandler onWindowLoaded = default(RoutedEventHandler)) where T : Window
        {
            T window;
            if (windows.ContainsKey(windowType))
                window = (T)windows[windowType];
            else
            {
                window = (T)container.Resolve(windowType);
                window.Closed += OnCloseWindowEvent;
                window.ShowInTaskbar = false;
                window.Owner = Application.Current.MainWindow;
                windows.Add(windowType, window);
            }
            
            if (windowSettings != null)
                windowSettings((T)window);

            if (onWindowLoaded != null)
                window.Loaded += onWindowLoaded;

            window.ShowDialog();
        }       
        public void RequestOpenWindow(Type windowType)
        {
            Window window;
            if (windows.ContainsKey(windowType))
                window = windows[windowType];
            else
            {
                window = (Window)container.Resolve(windowType);
                window.Closed += OnCloseWindowEvent;
                windows.Add(windowType, window);
            }

            window.Show();
        }
        public void RequestOpenWindowOneAtATime(Type windowType)
        {
            CloseAllWindows();
            Window window;
            if (windows.ContainsKey(windowType))
                window = windows[windowType];
            else
            {
                window = (Window)container.Resolve(windowType);
                window.Closed += OnCloseWindowEvent;
                windows.Add(windowType, window);
            }

            window.Show();
        }
        void OnCloseWindowEvent(object sender, EventArgs e)
        {
            windows.Remove(((Window)sender).GetType());
        }
        public void RequestCloseWindow(Type windowType)
        {
            if (windows.ContainsKey(windowType))
                windows[windowType].Close();
        }
        public void RequestHideWindow(Type windowType)
        {
            if (windows.ContainsKey(windowType))
                windows[windowType].Hide();
        }
        public void CloseAllWindows()
        {
            for (int i = windows.Count - 1; i >= 0; i--)
                windows.ElementAt(i).Value.Close();
        }
        public void RequestNavigate(string regionName, string viewName)
        {            
            regionManager.Regions[regionName].RequestNavigate(
                new Uri(viewName, UriKind.Relative));
        }
        public void RequestNavigate<T>(string regionName, string viewName, Action<T> viewModelSettings)
        {
            viewModelSettings(container.Resolve<T>());
            regionManager.Regions[regionName].RequestNavigate(
                new Uri(viewName, UriKind.Relative));
        }
    }
}