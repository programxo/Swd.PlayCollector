using Swd.PlayCollector.Gui.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Swd.PlayCollector.Gui.Wpf
{
    /// <summary>
    /// Interaction logic for fMain.xaml
    /// </summary>
    public partial class fMain : RibbonWindow
    {
        public object AuxiliaryPaneContent { get; set; }
        public fMain()
        {
            InitializeComponent();
            this.DataContext = new fMainViewModel();
        }
    }
}
