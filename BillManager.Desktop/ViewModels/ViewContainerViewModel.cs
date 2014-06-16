using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.UI;

namespace BillManager.Desktop.ViewModels
{
    public class ViewContainerViewModel : ViewModelBase
    {
        string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        double windowHeight;
        public double WindowHeight
        {
            get { return windowHeight; }
            set { windowHeight = value; }
        }
        double windowWidth;
        public double WindowWidth
        {
            get { return windowWidth; }
            set { windowWidth = value; }
        }
    }
}
