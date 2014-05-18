using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.DataMining
{
    public class StatisticOfLecturer : Statistic
    {
        private Lecturer _lecturer;

        public StatisticOfLecturer(ClassesSchedule schedule, Lecturer lecturer)
        {
            _lecturer = lecturer;
            _schedule = schedule;
            SetClasses();
            SetCounts();
        }

        private void SetClasses()
        {
            _classes = _schedule.Classes.Where(c => c.Lecturer == _lecturer);
        }
    }
}
