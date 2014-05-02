using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData.Interfaces;

namespace Editor.Models
{
    class TitlesMarkup
    {
        public List<TableItem<IHavingName>> Titles = new List<TableItem<IHavingName>>();

        public TitlesMarkup(IEnumerable<IHavingName> titles)
        {
            int currectCol = 0;
            foreach (var title in titles)
            {
                Titles.Add(new TableItem<IHavingName>(title){Row = 0, Column = currectCol});
                currectCol++;
            }
        }
    }
}
