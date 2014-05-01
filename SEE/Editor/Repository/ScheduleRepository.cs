using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;
using ScheduleData;
using ScheduleData.Editor;
using ScheduleData.Interfaces;

namespace Editor.Repository
{
    class ScheduleRepository
    {

        private static readonly Random Rnd = new Random();

        public static ISchedule Schedule = new Schedule();

        //public static int LecturesPerDay = 3;
        //public static int WeekdaysCount = 6;
        //public static int TimeLineLength = LecturesPerDay*WeekdaysCount;

        static ScheduleRepository()
        {
            InitTimeLine();
            InitClassrooms();
            InitSubjects();
            InitLecturers();
            InitYearsOfStudy();
            InitSpecializations();
            InitGroups();
            InitClasses();
        }

        private static void InitTimeLine()
        {
            for (int i = 0; i < WeekdaysCount; i++)
            {
                var day = (Weekdays)i;
                Schedule.TimeLine.Add(new ClassTime(WeekType.Both, day, new Time(9, 30), new Time(11, 05)));
                Schedule.TimeLine.Add(new ClassTime(WeekType.Both, day, new Time(11, 15), new Time(12, 50)));
                Schedule.TimeLine.Add(new ClassTime(WeekType.Both, day, new Time(13, 40), new Time(15, 15)));
                Schedule.TimeLine.Add(new ClassTime(WeekType.Both, day, new Time(15, 25), new Time(17, 00)));
            }
        }

        private static void InitClassrooms()
        {
            const int classroomsCount = 50;
            const int minRoomNumber = 1000;
            const int maxRoomNumber = 3007;

            for (int i = 0; i < classroomsCount; i++)
            {
                int num = Rnd.Next(minRoomNumber, maxRoomNumber);
                Schedule.Classrooms.Add(new Classroom(num.ToString(CultureInfo.InvariantCulture)));
            }
        }

        private static void InitSubjects()
        {
            string[] subjectNames = { "Matan", "Algebra", "Programming", "Diffirence Equations", "Math Logic", "Algorithms", "Functional Analisys" };
            int subjectsCount = subjectNames.Length;
            for (int i = 0; i < subjectsCount; i++)
            {
                Schedule.Subjects.Add(new Subject(subjectNames[i]));
            }
        }

        private static void InitLecturers()
        {
            string[] lecturerNames = { "Ivanov", "Petrov", "Baranov", "Semenov", "Kirilenko", "Polozov", "Luciv" };
            int lecturersCount = lecturerNames.Length;
            for (int i = 0; i < lecturersCount; i++)
            {
                Schedule.Lecturers.Add(new Lecturer(lecturerNames[i]));
            }
        }

        private static void InitYearsOfStudy()
        {
            const int yearsCount = 5;
            for (int i = 1; i < yearsCount; i++)
            {
                Schedule.YearsOfStudy.Add(new YearOfStudy(i.ToString(CultureInfo.InvariantCulture)));
            }
        }

        private static void InitSpecializations()
        {
            string[] specializationNames = { "Primat", "Matobess", "PI", "Pure math", "Mechanics", "Astronoms", "Kids" };
            int specializationCount = specializationNames.Length;
            for (int i = 0; i < specializationCount; i++)
            {
                Schedule.Specializations.Add(new Specialization(specializationNames[i]));
            }
        }

        private static void InitGroups()
        {
            string[] groupNames = { "111", "112", "221", "223", "242", "271", "42" };
            int groupCount = groupNames.Length;
            var years = Schedule.YearsOfStudy.GetAll().ToArray();
            var specs = Schedule.Specializations.GetAll().ToArray();
            for (int i = 0; i < groupCount; i++)
            {
                Schedule.Groups.Add(new Group(groupNames[i], years[0], specs[Rnd.Next(specs.Length - 1)]));
            }
        }

        private static void InitClasses()
        {
            var subjs = Schedule.Subjects.GetAll().ToArray();
            var lectr = Schedule.Lecturers.GetAll().ToArray();
            var groups = Schedule.Groups.GetAll().ToArray();
            var times = Schedule.TimeLine.GetAll().ToArray();
            var rooms = Schedule.Classrooms.GetAll().ToArray();

            int classCount = groups.Length*times.Length/2;
            for (int i = 0; i < classCount; i++)
            {
                var s = subjs[Rnd.Next(subjs.Length - 1)];
                var l = lectr[Rnd.Next(lectr.Length - 1)];
                var r = rooms[Rnd.Next(rooms.Length - 1)];

                IGroup g = groups[Rnd.Next(groups.Length - 1)];
                IClassTime t = times[Rnd.Next(times.Length - 1)];
                int helper = 0;
                while (Schedule.Classes.Get(g, t) != null)
                {
                    g = groups[Rnd.Next(groups.Length - 1)];
                    t = times[Rnd.Next(times.Length - 1)];
                    if (helper++ > 1000000) break;
                }
                Schedule.Classes.Add(new Class(s, g, l, r, t));
            }
        }
        
    }
}
