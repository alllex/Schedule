using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models
{
    class SpanedItem<T>
    {

        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public T Item { get; set; }

        public SpanedItem(T t)
        {
            Item = t;
            RowSpan = 1;
            ColumnSpan = 1;
        }

        public SpanedItem()
        {
            RowSpan = 1;
            ColumnSpan = 1;
        } 

        public bool IsSpaned()
        {
            return RowSpan != 1 || ColumnSpan != 1;
        }

        public override bool Equals(object obj)
        {
            var t = (SpanedItem<T>)obj;
            if (t == null) return false;
            return Item.Equals(t.Item);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode();
        }
    }
}
