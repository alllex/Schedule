using System;
using System.Globalization;
using System.Windows;
using Editor.Models;

namespace Editor.Repository
{
    class ScheduleRepository
    {

        private static readonly Random Rnd = new Random();

        public ClassesSchedule Schedule = new ClassesSchedule();

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
            var wts = Enum.GetValues(typeof(WeekType));
            foreach (var weekday in wds)
            {
                foreach (var wt in wts)
                {
                    Schedule.TimeLine.Add(new ClassTime { Week = (WeekType)wt, Day = (Weekdays)weekday, BeginTime = new Time(9, 00), EndTime = new Time(10, 00) });
                    Schedule.TimeLine.Add(new ClassTime { Week = (WeekType)wt, Day = (Weekdays)weekday, BeginTime = new Time(11, 00), EndTime = new Time(12, 00) });
                    //Schedule.TimeLine.Add(new ClassTime { Week = (WeekType)wt, Day = (Weekdays)weekday, BeginTime = new Time(13, 00), EndTime = new Time(14, 00) });
                    //Schedule.TimeLine.Add(new ClassTime { Week = (WeekType)wt, Day = (Weekdays)weekday, BeginTime = new Time(15, 00), EndTime = new Time(16, 00) });
                }
            }
        }

        private void InitClassrooms()
        {
            const int classroomsCount = 20;
            const int minRoomNumber = 1000;
            const int maxRoomNumber = 3007;

            for (int i = 0; i < classroomsCount; i++)
            {
                int num = Rnd.Next(minRoomNumber, maxRoomNumber);
                Schedule.Classrooms.Add(new Classroom{Name = num.ToString(CultureInfo.InvariantCulture), Address = "Seasam street"});
            }
        }

        private void InitSubjects()
        {
            string[] subjectNames = { "Matan", "Algebra", "Programming", "Diffirence Equations", "Math Logic", "Algorithms", "Functional Analisys" };
            int subjectsCount = subjectNames.Length;
            for (int i = 0; i < subjectsCount; i++)
            {
                Schedule.Subjects.Add(new Subject{Name = subjectNames[i]});
            }
        }

        private void InitLecturers()
        {
            string[] lecturerNames = { "Ivanov", "Petrov", "Baranov", "Semenov", "Kirilenko", "Polozov", "Luciv" };
            int lecturersCount = lecturerNames.Length;
            for (int i = 0; i < lecturersCount; i++)
            {
                Schedule.Lecturers.Add(new Lecturer{Name = lecturerNames[i]});
            }
        }

        private void InitYearsOfStudy()
        {
            const int yearsCount = 1;
            for (int i = 1; i <= yearsCount; i++)
            {
                Schedule.YearsOfStudy.Add(new YearOfStudy{Name = i.ToString(CultureInfo.InvariantCulture)});
            }
        }

        private void InitSpecializations()
        {
            string[] specializationNames = { "Primat", "Matobess", "PI", "Pure math" };
            int specializationCount = specializationNames.Length;
            for (int i = 0; i < specializationCount; i++)
            {
                Schedule.Specializations.Add(new Specialization{Name = specializationNames[i]});
            }
        }

        private void InitGroups()
        {
            string[] groupNames = { "A", "B", "C", "D"/*, "E", "F", "G", "H", "I", "J", "K", "L", "M", "N"*/ };
            int groupCount = groupNames.Length;
            for (int i = 0; i < groupCount; i++)
            {
                var g = groupNames[i];
                var y = Schedule.YearsOfStudy[Rnd.Next(Schedule.YearsOfStudy.Count)];
                var s = Schedule.Specializations[Rnd.Next(Schedule.Specializations.Count)];
                Schedule.Groups.Add(new Group{Name = g, YearOfStudy = y, Specialization = s});
            }
        }

        private void InitClasses()
        {
            int classCount = Schedule.Groups.Count*Schedule.TimeLine.Count * 3 / 5;
            for (int i = 0; i < classCount; i++)
            {
                Subject s = Schedule.Subjects[Rnd.Next(Schedule.Subjects.Count)];
                ClassTime t = Schedule.TimeLine[Rnd.Next(Schedule.TimeLine.Count)];
                Group g = Schedule.Groups[Rnd.Next(Schedule.Groups.Count)];
                Lecturer l = Schedule.Lecturers[Rnd.Next(Schedule.Lecturers.Count)];
                Classroom c = Schedule.Classrooms[Rnd.Next(Schedule.Classrooms.Count)];
                int helper = 0;
                var @class = new Class {Classroom = c, ClassTime = t, Group = g, Lecturer = l, Subject = s};
                while (Schedule.Classes.Contains(@class))
                {
                    s = Schedule.Subjects[Rnd.Next(Schedule.Subjects.Count)];
                    t = Schedule.TimeLine[Rnd.Next(Schedule.TimeLine.Count)];
                    g = Schedule.Groups[Rnd.Next(Schedule.Groups.Count)];
                    l = Schedule.Lecturers[Rnd.Next(Schedule.Lecturers.Count)];
                    c = Schedule.Classrooms[Rnd.Next(Schedule.Classrooms.Count)];
                    @class = new Class { Classroom = c, ClassTime = t, Group = g, Lecturer = l, Subject = s };
                    if (helper++ > 20)
                    {
                        MessageBox.Show("Helper");
                        break;
                    }
                }
                Schedule.Classes.Add(@class);
            }
        }
        
    }
}
