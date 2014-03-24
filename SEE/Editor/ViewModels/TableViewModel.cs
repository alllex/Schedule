using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Editor.Helpers;

namespace Editor.ViewModels
{
    class TableRecord : NotificationObject
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    RaisePropertyChanged(() => Data);
                }
            }
        }

        private bool _isEdited;
        public bool IsEdited
        {
            get { return _isEdited; }
            set
            {
                if (_isEdited != value)
                {
                    _isEdited = value;
                    RaisePropertyChanged(() => IsEdited);
                }
            }
        }
    }

    class TableViewModel : BaseViewModel
    {
        private String[,] table;
        private ObservableCollection<TableRecord> _tableRecords = new ObservableCollection<TableRecord>();

        public TableViewModel()
        {
            const int rows = 3;
            const int cols = 3;
            table = new string[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    table[i, j] = String.Format("[{0}, {1}]", i + 1, j + 1);
                }
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _tableRecords.Add(new TableRecord{RowIndex = i, ColumnIndex = j, Data = table[i, j]});
                }
            }
        }

        public int RowCount
        {
            get { return table.GetLength(0); }
        }

        public int ColumnCount
        {
            get { return table.GetLength(1); }
        }

        public ObservableCollection<TableRecord> TableRecords
        {
            get { return _tableRecords; }
        }
    }
}
