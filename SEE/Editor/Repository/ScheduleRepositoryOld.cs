

namespace Editor.Repository
{

    //class ScheduleRepository
    //{

    //    private static readonly Random Rnd = new Random();

    //    public static ISchedule Schedule = new Schedule();

    //    static ScheduleRepository()
    //    {
    //        InitTimeLine();
    //        InitClassrooms();
    //        InitSubjects();
    //        InitLecturers();
    //        InitYearsOfStudy();
    //        InitSpecializations();
    //        InitGroups();
    //        InitClasses();
    //    }

    //    private static void InitTimeLine()
    //    {
    //        var wds = Enum.GetValues(typeof(Weekdays));
    //        var wts = Enum.GetValues(typeof(WeekType));
    //        foreach (var weekday in wds)
    //        {
    //            foreach (var wt in wts)
    //            {
    //                Schedule.TimeLine.Add(new ClassTime((WeekType)wt, (Weekdays)weekday, new Time(9, 30),  new Time(11, 05)));
    //                Schedule.TimeLine.Add(new ClassTime((WeekType)wt, (Weekdays)weekday, new Time(11, 15), new Time(12, 50)));
    //                Schedule.TimeLine.Add(new ClassTime((WeekType)wt, (Weekdays)weekday, new Time(13, 40), new Time(15, 15)));
    //                Schedule.TimeLine.Add(new ClassTime((WeekType)wt, (Weekdays)weekday, new Time(15, 25), new Time(17, 00)));
    //            }
    //        }
    //    }

    //    private static void InitClassrooms()
    //    {
    //        const int classroomsCount = 50;
    //        const int minRoomNumber = 1000;
    //        const int maxRoomNumber = 3007;

    //        for (int i = 0; i < classroomsCount; i++)
    //        {
    //            int num = Rnd.Next(minRoomNumber, maxRoomNumber);
    //            Schedule.Classrooms.Add(new Classroom(num.ToString(CultureInfo.InvariantCulture)){Address = "Seasam street"});
    //        }
    //    }

    //    private static void InitSubjects()
    //    {
    //        string[] subjectNames = { "Matan", "Algebra", "Programming", "Diffirence Equations", "Math Logic", "Algorithms", "Functional Analisys" };
    //        int subjectsCount = subjectNames.Length;
    //        for (int i = 0; i < subjectsCount; i++)
    //        {
    //            Schedule.Subjects.Add(new Subject(subjectNames[i]));
    //        }
    //    }

    //    private static void InitLecturers()
    //    {
    //        string[] lecturerNames = { "Ivanov", "Petrov", "Baranov", "Semenov", "Kirilenko", "Polozov", "Luciv" };
    //        int lecturersCount = lecturerNames.Length;
    //        for (int i = 0; i < lecturersCount; i++)
    //        {
    //            Schedule.Lecturers.Add(new Lecturer(lecturerNames[i]));
    //        }
    //    }

    //    private static void InitYearsOfStudy()
    //    {
    //        const int yearsCount = 1;
    //        for (int i = 1; i <= yearsCount; i++)
    //        {
    //            Schedule.YearsOfStudy.Add(new YearOfStudy(i.ToString(CultureInfo.InvariantCulture)));
    //        }
    //    }

    //    private static void InitSpecializations()
    //    {
    //        string[] specializationNames = { "Primat", "Matobess", "PI", "Pure math", "Mechanics", "Astronoms", "Kids" };
    //        int specializationCount = specializationNames.Length;
    //        for (int i = 0; i < specializationCount; i++)
    //        {
    //            Schedule.Specializations.Add(new Specialization(specializationNames[i]));
    //        }
    //    }

    //    private static void InitGroups()
    //    {
    //        string[] groupNames = { "111", "112", "221", "223", "242", "271", "42" };
    //        int groupCount = groupNames.Length;
    //        var years = Schedule.YearsOfStudy.GetAll().ToArray();
    //        var specs = Schedule.Specializations.GetAll().ToArray();
    //        for (int i = 0; i < groupCount; i++)
    //        {
    //            var g = groupNames[i];
    //            var y = years[0];
    //            var s = specs[Rnd.Next(specs.Length)];
    //            Schedule.Groups.Add(new Group(g, y, s));
    //        }
    //    }

    //    private static void InitClasses()
    //    {
    //        var subjs = Schedule.Subjects.GetAll().ToArray();
    //        var lectr = Schedule.Lecturers.GetAll().ToArray();
    //        var groups = Schedule.Groups.GetAll().ToArray();
    //        var times = Schedule.TimeLine.GetAll().ToArray();
    //        var rooms = Schedule.Classrooms.GetAll().ToArray();

    //        int classCount = groups.Length*times.Length/2;
    //        for (int i = 0; i < classCount; i++)
    //        {
    //            var s = subjs[Rnd.Next(subjs.Length - 1)];

    //            IClassTime t = times[Rnd.Next(times.Length)];
    //            IGroup g = groups[Rnd.Next(groups.Length)];
    //            ILecturer l = lectr[Rnd.Next(lectr.Length)];
    //            IClassroom r = rooms[Rnd.Next(rooms.Length)];
    //            int helper = 0;
    //            while (Schedule.Classes.Get(g, t) != null || Schedule.Classes.Get(r, t) != null || Schedule.Classes.Get(l, t) != null)
    //            {
    //                g = groups[Rnd.Next(groups.Length)];
    //                t = times[Rnd.Next(times.Length)];
    //                l = lectr[Rnd.Next(lectr.Length)];
    //                r = rooms[Rnd.Next(rooms.Length)];
    //                if (helper++ > 1000000)
    //                {
    //                    MessageBox.Show("Helper");
    //                    break;
    //                }
    //            }
    //            Schedule.Classes.Add(new Class(s, g, l, r, t));
    //        }
    //    }
        
    //}
}
