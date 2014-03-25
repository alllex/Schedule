using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Editor.Helpers;

namespace Editor.ViewModels
{
    class TableRecord : NotificationObject
    {
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
    }

    class TableViewModel : BaseViewModel
    {
        private const int Rows = 3;
        private const int Cols = 3;
        private DataTable table = new DataTable();

        public TableViewModel()
        {
            for (int j = 0; j < Cols; j++)
            {
                table.Columns.Add(String.Format("Column {0}", j));
            }
            for (int i = 0; i < Rows; i++)
            {
                var arr = new object[Cols];
                for (int j = 0; j < Cols; j++)
                {
                    arr[j] = new TableRecord{Data = String.Format("[{0}, {1}]", i, j)};
                }
                table.Rows.Add(arr);
            }
        }

        public int RowCount
        {
            get { return Rows; }
        }

        public int ColumnCount
        {
            get { return Cols; }
        }

        public DataTable TableRecords
        {
            get { return table; }
        }
    }
}
