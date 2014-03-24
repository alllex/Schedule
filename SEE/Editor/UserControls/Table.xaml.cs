using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Editor.Helpers;
using Editor.ViewModels;
using Editor.Views;

namespace Editor.UserControls
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
            /*
            itemsControl.ItemsSource = new[]
            {
                new { RowIndex = 0, ColumnIndex = 0, Data = "abc"},
                new { RowIndex = 1, ColumnIndex = 1, Data = "bbb"},
                new { RowIndex = 2, ColumnIndex = 2, Data = "ccc"}
            };
            ItemsControl itemsControl = new ItemsControl();
            GridEx grid = new GridEx(){RowCount = 3, ColumnCount = 3};
            itemsControl.ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(GridEx))); 
            
            var model = itemsControl.DataContext as TableViewModel;
            if (model == null)
            {
                MessageBox.Show("Fail...");
                return;
            }
            itemsControl.ItemsSource = model.TableRecords;
            itemsControl.ItemContainerGenerator.StatusChanged += (e, a) =>
            {
                var gen = e as ItemContainerGenerator; 
                if (gen == null)
                {
                    MessageBox.Show("WTF");
                    return;
                }
                if (gen.Status == GeneratorStatus.ContainersGenerated)
                {
                    if (_grid == null)
                    {
                        MessageBox.Show("Incorrect order!!((((");
                        return;
                    }
                    _grid.RowCount = model.RowCount;
                    _grid.ColumnCount = model.ColumnCount;
                }
            };
             */
        }

        /*
        private GridEx _grid = null;

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            _grid = sender as GridEx;
        }
         */

        private void TableElem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBlock;
            if (tb == null) return;
            if (e.ClickCount >= 2)
            {
                var dc = tb.DataContext as TableRecord;
                if (dc == null) return;
                var ecw = new EditCardWindow(this){DataContext = dc};
                IsEnabled = false;
                ecw.Show();
            }
        }
    }
}
