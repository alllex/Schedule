namespace ScheduleData.DataMining
{
    public class DataMiner
    {
        public static StatisticOfGroup StaticticOfGroup(Schedule schedule, Group group)
        {
            return new StatisticOfGroup(schedule, group);
        }

        public static StatisticOfLecturer StaticticOfLecturer(Schedule schedule, Lecturer lecturer)
        {
            return new StatisticOfLecturer(schedule, lecturer);
        }

        public static StatisticOfClassroom StaticticOfClassroom(Schedule schedule, Classroom classroom)
        {
            return new StatisticOfClassroom(schedule, classroom);
        }

        public static StatisticOfTime StaticticOfTime(Schedule schedule, ClassTime time)
        {
            return new StatisticOfTime(schedule, time);
        }

        public static StatisticOfSubject StaticticOfSubject(Schedule schedule, Subject subject)
        {
            return new StatisticOfSubject(schedule, subject);
        }
    }
}
