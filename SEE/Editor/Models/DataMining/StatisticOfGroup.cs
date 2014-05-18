using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.DataMining
{
    public class StatisticOfGroup : Statistic
    {
        private Group _group;

        public StatisticOfGroup(ClassesSchedule schedule, Group group)
        {
            _group = group;
            _schedule = schedule;
            SetClasses();
            SetCounts();
        }

        private void SetClasses()
        {
            _classes = _schedule.Classes.Where(c => c.Group == _group);
        }
    }
}