using System.Collections.Generic;
using System.Linq;

namespace ScheduleData.DataMining
{
    public class StatisticOfClassroom : Statistic<Classroom>
    {
        public Dictionary<ClassTime, int> CountGroupsInClassroom;
        
        public StatisticOfClassroom(Schedule schedule, Classroom subject)
            : base(schedule, subject, f => f == null ? null : f.Classroom) 
        {
            CountGroupsInClassroom = new Dictionary<ClassTime, int>(schedule.TimeLine.Count);
            SetCountGroupsInClassroom();
        }

        private void SetCountGroupsInClassroom()
        {
            var groups = from c in Classes
                         group c by c.ClassTime;

            foreach (var t in Schedule.TimeLine)
                CountGroupsInClassroom.Add(t, 0);

            foreach (var g in groups)
            {
                CountGroupsInClassroom.Remove(g.Key);
                CountGroupsInClassroom.Add(g.Key, g.Count());
            }
        }
    }

    public class StatisticOfGroup : Statistic<Group>
    {
        public StatisticOfGroup(Schedule schedule, Group subject)
            : base(schedule, subject, f => f == null ? null : f.Group) { }
    }

    public class StatisticOfLecturer  : Statistic<Lecturer>
    {
        public StatisticOfLecturer(Schedule schedule, Lecturer subject)
            : base(schedule, subject, f => f == null ? null : f.Lecturer) { }
    }

    public class StatisticOfTime      : Statistic<ClassTime>
    {
        public StatisticOfTime(Schedule schedule, ClassTime subject)
            : base(schedule, subject, f => f == null ? null : f.ClassTime) { }
    }

    public class StatisticOfSubject   : Statistic<Subject>
    {
        public StatisticOfSubject(Schedule schedule, Subject subject)
            : base(schedule, subject, f => f == null ? null : f.Subject) { }
    }
}
