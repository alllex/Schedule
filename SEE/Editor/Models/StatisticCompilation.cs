using System;
using System.Collections.ObjectModel;
using Editor.Models.DataMining;

namespace Editor.Models
{
    public class StatisticCompilation
    {

        public Collection<Statistic<Group>> GroupStatistic;
        public Collection<Statistic<Lecturer>> LecturerStatistic;

        public StatisticCompilation(ClassesSchedule schedule)
        {
            BuildGroupStatistics(schedule);
            BuildLecturerStatistics(schedule);
        }

        private void BuildLecturerStatistics(ClassesSchedule schedule)
        {
            LecturerStatistic = new Collection<Statistic<Lecturer>>();
            foreach (var lecturer in schedule.Lecturers)
            {
                var stat = DataMiner.StaticticOfLecturer(schedule, lecturer);
                LecturerStatistic.Add(stat);
            }
        }

        private void BuildGroupStatistics(ClassesSchedule schedule)
        {
            GroupStatistic = new Collection<Statistic<Group>>();
            foreach (var group in schedule.Groups)
            {
                var stat = DataMiner.StaticticOfGroup(schedule, group);
                GroupStatistic.Add(stat);
            }
        }
    }
}
