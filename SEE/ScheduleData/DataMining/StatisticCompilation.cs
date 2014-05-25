using System.Collections.ObjectModel;

namespace ScheduleData.DataMining
{
    public class StatisticCompilation
    {

        public Collection<Statistic<Group>> GroupStatistic = new Collection<Statistic<Group>>();
        public Collection<Statistic<Lecturer>> LecturerStatistic = new Collection<Statistic<Lecturer>>();

        public StatisticCompilation(Schedule schedule)
        {
            BuildAllStatistic(schedule);
        }

        public void UpdateStatistic(Schedule schedule)
        {
            BuildAllStatistic(schedule);
        }

        private void BuildAllStatistic(Schedule schedule)
        {
            BuildGroupStatistics(schedule);
            BuildLecturerStatistics(schedule);
        }

        private void BuildLecturerStatistics(Schedule schedule)
        {
            LecturerStatistic.Clear();
            foreach (var lecturer in schedule.Lecturers)
            {
                var stat = DataMiner.StaticticOfLecturer(schedule, lecturer);
                LecturerStatistic.Add(stat);
            }
        }

        private void BuildGroupStatistics(Schedule schedule)
        {
            GroupStatistic.Clear();
            foreach (var group in schedule.Groups)
            {
                var stat = DataMiner.StaticticOfGroup(schedule, group);
                GroupStatistic.Add(stat);
            }
        }
    }
}
