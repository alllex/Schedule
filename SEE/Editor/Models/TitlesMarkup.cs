﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Animation;
using Editor.Helpers;

namespace Editor.Models
{
    class TitlesMarkup
    {
        //public List<TableItem<HavingName>> Headers = new List<TableItem<HavingName>>();
        public List<TableItem<HavingName>> Titles = new List<TableItem<HavingName>>();

        public TitlesMarkup(IEnumerable<Group> groups)
        {
            var headers =
                from t in groups
                orderby t.Name
                group t by t.Specialization
                into h
                select h;

            int currectCol = 0;
            foreach (var h in headers)
            {
                Titles.Add(new TableItem<HavingName>{Item = h.Key, Column = currectCol, Row = 0, ColumnSpan = h.Count(), RowSpan = 1});
                foreach (var title in h)
                {
                    Titles.Add(new TableItem<HavingName> { Item = title, Row = 1, Column = currectCol });
                    currectCol++;
                }
            }
        }
    }
}
