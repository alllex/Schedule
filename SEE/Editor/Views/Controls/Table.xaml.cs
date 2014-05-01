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

        TableViewModel tableViewModel = new TableViewModel();

        public Table()
        {
            InitializeComponent();
            tableGrid.Loaded += TableGridOnLoaded;
        }
        
        private void TableGridOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ResizeGrid(tableViewModel.TableHeight(), tableViewModel.TableWidth());
            tableGrid.Children.Clear();
            foreach (var lecture in tableViewModel.LectureCards)
            {
                tableGrid.Children.Add(lecture);
            }
            foreach (var time in tableViewModel.TimeLine)
            {
                tableGrid.Children.Add(time);
            }
            foreach (var day in tableViewModel.DayLine)
            {
                tableGrid.Children.Add(day);
            }
            foreach (var title in tableViewModel.Titles)
            {
                tableGrid.Children.Add(title);
            }
            foreach (var subtitle in tableViewModel.Subtitles)
            {
                tableGrid.Children.Add(subtitle);
            }
        }

        private void ResizeGrid(int rowCount, int colCount)
        {
            tableGrid.RowDefinitions.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                tableGrid.RowDefinitions.Add(new RowDefinition{Height = GridLength.Auto});
            }
            for (int i = 0; i < colCount; i++)
            {
                tableGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = GridLength.Auto});
            }
        }
         


    }
}
