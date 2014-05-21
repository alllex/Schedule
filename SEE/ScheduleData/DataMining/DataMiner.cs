namespace ScheduleData.DataMining
{
    public class DataMiner
    {
        public static StatisticOfGroup StaticticOfGroup(ClassesSchedule schedule, Group group)
        {
            return new StatisticOfGroup(schedule, group);
        }

        public static StatisticOfLecturer StaticticOfLecturer(ClassesSchedule schedule, Lecturer lecturer)
        {
            return new StatisticOfLecturer(schedule, lecturer);
        }

        public static StatisticOfClassroom StaticticOfClassroom(ClassesSchedule schedule, Classroom classroom)
        {
            return new StatisticOfClassroom(schedule, classroom);
        }

        public static StatisticOfTime StaticticOfTime(ClassesSchedule schedule, ClassTime time)
        {
            return new StatisticOfTime(schedule, time);
        }

        public static StatisticOfSubject StaticticOfSubject(ClassesSchedule schedule, Subject subject)
        {
            return new StatisticOfSubject(schedule, subject);
        }
    }
}
