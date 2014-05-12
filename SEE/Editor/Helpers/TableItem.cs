using Editor.Models;

namespace Editor.Helpers
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

        public TableItem()
        {
            Row = 0;
            Column = 0;
        } 
    }
}
