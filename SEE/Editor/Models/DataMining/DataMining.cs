using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Repository;

namespace Editor.Models.DataMining
{
    public class DataMining
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
    }
}
