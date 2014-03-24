using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Editor.Helpers
{
    class GridEx : Grid
    {
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(GridEx));
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(GridEx));
        
        public int RowCount
        {
            get { return (int) GetValue(RowCountProperty);  }
            set
            {
                RowDefinitions.Clear();
                for (int i = 0; i < value; i++)
                    RowDefinitions.Add(new RowDefinition());
                SetValue(RowCountProperty, value);
            }
        }

        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set
            {
                ColumnDefinitions.Clear();
                for (int i = 0; i < value; i++)
                    ColumnDefinitions.Add(new ColumnDefinition());
                SetValue(ColumnCountProperty, value);
            }
        }
    }
}
