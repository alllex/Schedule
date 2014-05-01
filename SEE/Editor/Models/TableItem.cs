using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models
{
    class TableItem<T> : SpanedItem<T>
    {

        public int Row { get; set; }
        public int Column { get; set; }

        public TableItem(T t) : base(t)
        {
            Row = 0;
            Column = 0;
        }
    }
}
