using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.DataMining
{
    public class StatisticOfClassroom : Statistic
    {
        private Classroom _classroom;

        public StatisticOfClassroom(ClassesSchedule schedule, Classroom classroom)
        {
            _classroom = classroom;
            _schedule = schedule;
            SetClasses();
            SetCounts();
        }

        private void SetClasses()
        {
            _classes = _schedule.Classes.Where(c => c.Classroom == _classroom);
        }
    }
}
