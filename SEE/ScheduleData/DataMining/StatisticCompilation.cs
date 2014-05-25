using System.Collections.ObjectModel;

namespace ScheduleData.DataMining
{
    public class StatisticCompilation
    {

        public Collection<Statistic<Group>> GroupStatistic = new Collection<Statistic<Group>>();
        public Collection<Statistic<Lecturer>> LecturerStatistic = new Collection<Statistic<Lecturer>>();
        public Collection<Statistic<Subject>> SubjectStatistic = new Collection<Statistic<Subject>>();
        public Collection<Statistic<Classroom>> ClassroomStatistic = new Collection<Statistic<Classroom>>();
        public Collection<Statistic<ClassTime>> ClassTimeStatistic = new Collection<Statistic<ClassTime>>();

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
            BuildSubjectStatistics(schedule);
            BuildClassroomStatistics(schedule);
            BuildClassTimeStatistics(schedule);
        }

        private void BuildClassTimeStatistics(Schedule schedule)
        {
            ClassTimeStatistic.Clear();
            foreach (var classTime in schedule.TimeLine)
            {
                var stat = DataMiner.StaticticOfTime(schedule, classTime);
                ClassTimeStatistic.Add(stat);
            }
        }

        private void BuildClassroomStatistics(Schedule schedule)
        {
            ClassroomStatistic.Clear();
            foreach (var classroom in schedule.Classrooms)
            {
                var stat = DataMiner.StaticticOfClassroom(schedule, classroom);
                ClassroomStatistic.Add(stat);
            }
        }

        private void BuildSubjectStatistics(Schedule schedule)
        {
            SubjectStatistic.Clear();
            foreach (var subject in schedule.Subjects)
            {
                var stat = DataMiner.StaticticOfSubject(schedule, subject);
                SubjectStatistic.Add(stat);
            }
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
