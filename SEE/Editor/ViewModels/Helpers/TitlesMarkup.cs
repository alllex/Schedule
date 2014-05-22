using System.Collections.Generic;
using System.Linq;
using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Helpers
{
    class TitlesMarkup
    {
        public List<TableItem<Specialization>> Titles = new List<TableItem<Specialization>>();
        public List<TableItem<Group>> Subtitles = new List<TableItem<Group>>();

        public TitlesMarkup(IEnumerable<Group> groups)
        {
            var headers =
                from t in groups
                where t.Specialization != null
                group t by t.Specialization;

            int currectCol = 0;
            foreach (var h in headers)
            {
                Titles.Add(new TableItem<Specialization> { Item = h.Key, Column = currectCol, Row = 0, ColumnSpan = h.Count(), RowSpan = 1 });
                foreach (var title in h)
                {
                    Subtitles.Add(new TableItem<Group> { Item = title, Row = 1, Column = currectCol });
                    currectCol++;
                }
            }
        }
    }
}
