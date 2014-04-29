using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;
using ScheduleData;

namespace Editor.Repository
{
    class ScheduleRepository
    {
        private static Random rnd = new Random();

        public static int LecturesPerDay = 3;
        public static int WeekdaysCount = 6;
        public static int TimeLineLength = LecturesPerDay*WeekdaysCount;

        public static IGroup[] Groups = { new Group("111", new List<IGroup> {new Group("111.1"), new Group("111.2"), new Group("111.3")}), 
                                          new Group("112", new List<IGroup> {new Group("112.1")}), 
                                          new Group("121", new List<IGroup> {new Group("121.1"), new Group("121.2")}), 
                                          new Group("171") };

        public static List<IGroup> Subgroups;
        public static int SubtitleCount;

        public static ITimeInterval[] TimeLine;

        public static ILecture[][] Table;

        static ScheduleRepository()
        {
            InitializeTimeLine();
            InitializeSubgroup();
            InitializeTable();
        }

        private static void InitializeSubgroup()
        {
            Subgroups = new List<IGroup>();
            foreach (var group in Groups)
            {
                int d = group.Subgroups.Count();
                if (d > 0)
                {
                    Subgroups.AddRange(group.Subgroups);
                }
                else
                {
                    Subgroups.Add(new Group("_"));
                }
                SubtitleCount += d == 0 ? 1 : d;
            }
        }

        private static void InitializeTable()
        {
            int rowCount = TimeLineLength;
            int colCount = SubtitleCount;
            Table = new ILecture[rowCount][];
            for (int r = 0; r < rowCount; r++)
            {
                Table[r] = new ILecture[colCount];
                for (int c = 0; c < colCount; c++)
                {
                    var lecture = new Lecture
                    {
                        Group = Subgroups[c],
                        Lecturer = new Lecturer { Name = RandomLecturerName() },
                        Subject = new Subject { Name = RandomSubjectNames() }
                    };
                    Table[r][c] = lecture;
                }
            }
        }

        private static void InitializeTimeLine()
        {
            TimeLine = new ITimeInterval[TimeLineLength];
            for (int i = 0; i < WeekdaysCount; i++)
            {
                var day = (Weekdays) i;
                TimeLine[i*LecturesPerDay] = new TimeInterval
                    {
                        Day = day,
                        Begin = new Time {Hours = 9, Minutes = 30},
                        End = new Time {Hours = 11, Minutes = 05}
                    };
                TimeLine[i * LecturesPerDay + 1] = new TimeInterval
                {
                    Day = day,
                    Begin = new Time { Hours = 11, Minutes = 15 },
                    End = new Time { Hours = 12, Minutes = 50 }
                };
                TimeLine[i * LecturesPerDay + 2] = new TimeInterval
                {
                    Day = day,
                    Begin = new Time { Hours = 13, Minutes = 40 },
                    End = new Time { Hours = 15, Minutes = 15 }
                };
            }
        }

        private static readonly string[] LecturerNames = {"Ivanov", "Petrov", "Baranov", "Semenov", "Kirilenko", "Polozov", "Luciv"};
        private static string RandomLecturerName()
        {
            int i = rnd.Next(LecturerNames.Count() - 1);
            return LecturerNames[i];
        }

        private static readonly string[] SubjectNames = { "Matan", "Algebra", "Programming", "Diffirence Equations", "Math Logic", "Algorithms", "Functional Analisys" };
        private static string RandomSubjectNames()
        {
            int i = rnd.Next(SubjectNames.Count() - 1);
            return SubjectNames[i];
        }

        public static int RowCount()
        {
            return TimeLineLength;
        }

        public static int ColCount()
        {
            return SubtitleCount;
        }
    }
}
