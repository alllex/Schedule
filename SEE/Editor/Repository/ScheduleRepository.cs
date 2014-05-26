﻿using System;
using System.Globalization;
using System.Linq;
using Editor.Models;
using ScheduleData;

namespace Editor.Repository
{
    class ScheduleRepository
    {

        private static readonly Random Rnd = new Random();

        public Schedule Schedule = new Schedule();

        public ScheduleRepository()
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

        private void InitTimeLine()
        {
            var wds = Enum.GetValues(typeof(Weekdays));
            foreach (var weekday in wds)
            {
                for (int i = 0; i < ClassTime.ClassIntervals.Count(); i++)
                {
                    Schedule.TimeLine.Add(new ClassTime { Day = (Weekdays)weekday, Number = i});
                }
            }
        }

        private void InitClassrooms()
        {
            const int classroomsCount = 100;
            const int minRoomNumber = 1000;
            const int maxRoomNumber = 2001;

            string[] addresses = {"Petergoff", "Saint-Petersburg"};

            for (int i = 0; i < classroomsCount; i++)
            {
                int num = Rnd.Next(minRoomNumber, maxRoomNumber);
                Schedule.Classrooms.Add(new Classroom
                {
                    Name = num.ToString(CultureInfo.InvariantCulture), 
                    Address = addresses[Rnd.Next(addresses.Count())]
                });
            }
        }

        private void InitSubjects()
        {
            string[] subjectNames =
            {
                "Matan", "Algebra", "Programming", "Diffirence Equations", "Math Logic", "Algorithms", "Functional Analisys",
                "Economics", "English", "History", "Software development"
            };
            int subjectsCount = subjectNames.Length;
            for (int i = 0; i < subjectsCount; i++)
            {
                Schedule.Subjects.Add(new Subject{Name = subjectNames[i]});
            }
        }

        private void InitLecturers()
        {
            string[] lecturerNames =
            {
                "Ivanov", "Petrov", "Sidirov", "Baranov", "Semenov", "Kirilenko", "Polozov", "Luciv",
                "Tarasov", "Nemeshev", "Tapochkin", "Hamilton"
            };
            int lecturersCount = lecturerNames.Length;
            for (int i = 0; i < lecturersCount; i++)
            {
                Schedule.Lecturers.Add(new Lecturer{Name = lecturerNames[i]});
            }
        }

        private void InitYearsOfStudy()
        {
            const int yearsStart = 1;
            const int yearsCount = 1;
            for (int i = yearsStart; i < yearsStart + yearsCount; i++)
            {
                Schedule.YearsOfStudy.Add(new YearOfStudy{Name = "Курс " + i.ToString(CultureInfo.InvariantCulture)});
            }
        }

        private void InitSpecializations()
        {
            string[] specializationNames = { "Фундаментальная математика и механика", 
                                             "Прикладная математика и информатика", 
                                             "Математическое обеспечение \nи администрирование информационных систем", 
                                             "Фундаментальная информатика \nи информационные технологии",
                                             "Программная инженерия"};

            int specializationCount = specializationNames.Length;
            for (int i = 0; i < specializationCount; i++)
            {
                Schedule.Specializations.Add(new Specialization{Name = specializationNames[i]});
            }
        }

        private void InitGroups()
        {
            string[][] groupNames =
            {
                new[] { "111 (ПОМИ)", "112", "113" }, 
                new[] { "121", "122", "123", "124" },
                new[] { "141", "142", "143", "144" },
                new[] { "161" },
                new[] { "171" }/*,
                new[] { "211 (ПОМИ)", "212", "213" }, 
                new[] { "221", "222", "223", "224" },
                new[] { "241", "242", "243", "244" },
                new[] { "261" },
                new[] { "271" }*/
            };
            int groupCount = groupNames.Length;
            int max = Schedule.Specializations.Count - 1;
            for (int i = 0; i < groupCount; i++)
            {
                for (int j = 0; j < groupNames[i].Length; j++)
                {
                    var g = groupNames[i][j];
                    var y = Schedule.YearsOfStudy[/*2 * i >= groupCount ? 1 : */0];
                    var s = Schedule.Specializations[i > max ? 0 : i];
                    Schedule.Groups.Add(new Group { Name = g, YearOfStudy = y, Specialization = s });
                }
            }
        }

//        private void InitClasses()
//        {
//            Schedule.CreateNewTables();
//            foreach (var classesTable in Schedule.Tables)
//            {
//                int classCount = classesTable.Groups.Count() * Schedule.TimeLine.Count;
//                for (int i = 0; i < classCount; i++)
//                {
//                    Subject s = Schedule.Subjects[Rnd.Next(Schedule.Subjects.Count)];
//                    Lecturer l = Schedule.Lecturers[Rnd.Next(Schedule.Lecturers.Count)];
//                    Classroom c = Schedule.Classrooms[Rnd.Next(Schedule.Classrooms.Count)];
//
//                    var timeIndex = Rnd.Next(Schedule.TimeLine.Count);
//                    var groupIndex = Rnd.Next(classesTable.Groups.Count());
//
//                    if (classesTable.Table[timeIndex][groupIndex] == null)
//                    {
//                        classesTable.Table[timeIndex][groupIndex] = new ClassRecord
//                        {
//                            Subject = s,
//                            Lecturer = l,
//                            Classroom = c
//                        };
//                    }
//
//                }
//            }
//            
//        }

        private void InitClasses()
        {
            //int classCount = Schedule.Groups.Count() * Schedule.TimeLine.Count;
            foreach (var classTime in Schedule.TimeLine)
            {
                foreach (var @group in Schedule.Groups)
                {
                    Subject s = Schedule.Subjects[Rnd.Next(Schedule.Subjects.Count)];
                    Lecturer l = Schedule.Lecturers[Rnd.Next(Schedule.Lecturers.Count)];
                    Classroom c = Schedule.Classrooms[Rnd.Next(Schedule.Classrooms.Count)];
                    if (Rnd.Next(100) < 40) continue;
                        var @class = new ClassRecord
                        {
                            Classroom = c,
                        Group = @group,
                            Subject = s,
                            Lecturer = l,
                            ClassTime = classTime
                        };
                        Schedule.ClassRecords.Add(@class);
                }
            }
            
        }
        
    }
}
