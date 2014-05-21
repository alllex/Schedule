using System.Collections.ObjectModel;

namespace Editor.Models.DataMining
{
    public class StatisticCompilation
    {

        public Collection<Statistic<Group>> GroupStatistic = new Collection<Statistic<Group>>();
        public Collection<Statistic<Lecturer>> LecturerStatistic = new Collection<Statistic<Lecturer>>();

        public StatisticCompilation(ClassesSchedule schedule)
        {
            BuildAllStatistic(schedule);
        }

        public void UpdateStatistic(ClassesSchedule schedule)
        {
            BuildAllStatistic(schedule);
        }

        private void BuildAllStatistic(ClassesSchedule schedule)
        {
            BuildGroupStatistics(schedule);
            BuildLecturerStatistics(schedule);
        }

        private void BuildLecturerStatistics(ClassesSchedule schedule)
        {
            LecturerStatistic.Clear();
            foreach (var lecturer in schedule.Lecturers)
            {
                var stat = DataMiner.StaticticOfLecturer(schedule, lecturer);
                LecturerStatistic.Add(stat);
            }
        }

        private void BuildGroupStatistics(ClassesSchedule schedule)
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
