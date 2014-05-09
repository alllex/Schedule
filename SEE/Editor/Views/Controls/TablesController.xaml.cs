using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor.Views.Controls
{
    /// <summary>
    /// Interaction logic for TablesController.xaml
    /// </summary>
    public partial class TablesController : UserControl
    {
        public TablesController()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            Debug.WriteLine(GetType() + @": new DC = " + (DataContext == null ? "null" : DataContext.GetType().ToString()));
        }
    }
}
