using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Editor.Repository;
using Editor.ViewModels;

namespace Editor.UserControls
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {

        private static int amount = 0;
        private int num;

        public Table()
        {
            num = amount++;
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            UpdateDataContext();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            UpdateDataContext();
        }

        private void UpdateDataContext()
        {
            var tableViewModel = DataContext as TableViewModel;
            if (tableViewModel == null) return;
            ReorganizeChildren(tableViewModel);
        }

        private void ReorganizeChildren(TableViewModel tableViewModel)
        {
            ResizeGrid(tableViewModel.TableHeight(), tableViewModel.TableWidth());
            TableGrid.Children.Clear();
            foreach (var lecture in tableViewModel.ClassCards)
            {
                TableGrid.Children.Add(lecture);
            }
            foreach (var time in tableViewModel.TimeIntervals)
            {
                TableGrid.Children.Add(time);
            }
            foreach (var day in tableViewModel.DayLine)
            {
                TableGrid.Children.Add(day);
            }
            foreach (var title in tableViewModel.Titles)
            {
                TableGrid.Children.Add(title);
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
