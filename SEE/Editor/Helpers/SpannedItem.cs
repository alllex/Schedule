namespace Editor.Helpers
{
    class SpannedItem<T>
    {

        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public T Item { get; set; }

        public SpannedItem(T t)
        {
            Item = t;
            RowSpan = 1;
            ColumnSpan = 1;
        }

        public SpannedItem()
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
            var t = (SpannedItem<T>)obj;
            if (t == null) return false;
            return Item.Equals(t.Item);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode();
        }
    }
}
