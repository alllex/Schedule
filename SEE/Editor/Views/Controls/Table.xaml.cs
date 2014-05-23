using System.Windows;
using System.Windows.Controls;
using Editor.ViewModels.Controls;

namespace Editor.Views.Controls
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {

        public Table()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            UpdateDataContext();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateDataContext();
        }

        private void UpdateDataContext()
        {
            var tableViewModel = DataContext as TableViewModel;
            if (tableViewModel == null) return;
            ReorganizeChildren(tableViewModel);
        }

        private void ReorganizeChildren(TableViewModel model)
        {
            ResizeGrid(model.TableHeight(), model.TableWidth());
            TableGrid.Children.Clear();
            TableGrid.Children.Add(model.LeftTopControl);
            foreach (var time in model.TimeIntervals)
            {
                TableGrid.Children.Add(time);
            }
            foreach (var day in model.DayLine)
            {
                TableGrid.Children.Add(day);
            }
            foreach (var title in model.Titles)
            {
                TableGrid.Children.Add(title);
            }
            for (int row = 0; row < model.ClassesRowsCount; row++)
            {
                for (int col = 0; col < model.ClassesColumnsCount; col++)
                {
                    var card = model.ClassesCards[row][col];
                    if (card != null)
                        TableGrid.Children.Add(card);
                }
            }
        }
        
        private void ResizeGrid(int rowCount, int colCount)
        {
            TableGrid.RowDefinitions.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                TableGrid.RowDefinitions.Add(new RowDefinition{Height = GridLength.Auto});
            }
            for (int i = 0; i < colCount; i++)
            {
                TableGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = GridLength.Auto});
            }
        }
             
    }
}
