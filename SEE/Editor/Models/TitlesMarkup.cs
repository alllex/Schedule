using System.Collections.Generic;
using Editor.Helpers;

namespace Editor.Models
{
    class TitlesMarkup
    {
        public List<TableItem<HavingName>> Titles = new List<TableItem<HavingName>>();

        public TitlesMarkup(IEnumerable<HavingName> titles)
        {
            int currectCol = 0;
            foreach (var title in titles)
            {
                Titles.Add(new TableItem<HavingName>(title){Row = 0, Column = currectCol});
                currectCol++;
            }
        }
    }
}
