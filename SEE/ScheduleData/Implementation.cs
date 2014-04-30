using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleData
{
    public class TimeInterval: ITimeInterval
    {
        public Weekdays Day { get; set; }
        public Time Begin { get; set; }
        public Time End { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-\n{1}", Begin, End);
        }
    }

    public class LectureTime : ILectureTime
    {
        public WeekType Week { get; set; }
        public Weekdays Day { get; set; }
        public Time Begin { get; set; }
        public Time End { get; set; }
    }

    public class Subject: ISubject, IHavingId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Room : IRoom, IHavingId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Lecturer : ILecturer, IHavingId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Department { get; set; }
    }

    public class Group : IGroup, IHavingId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IGroup Overgroup { get; set; }
        public IList<IGroup> Subgroups { get; set; }

        public Group(string name)
        {
            Name = name;
            Subgroups = new List<IGroup>();
        }

        public Group(string name, IGroup overgroup)
        {
            Name = name;
            Subgroups = new List<IGroup>();
            Overgroup = overgroup;
        }

        public Group(string name, IEnumerable<IGroup> subgroups)
        {
            Name = name;
            Subgroups = new List<IGroup>(subgroups);
        }

        public Group(string name, IEnumerable<IGroup> subgroups, IGroup overgroup)
        {
            Name = name;
            Subgroups = new List<IGroup>(subgroups);
            Overgroup = overgroup;
        }
    }

    public class Lecture : ILecture, IHavingId
    {
        public int Id { get; set; }
        public ISubject Subject { get; set; }
        public IGroup Group { get; set; }
        public ILecturer Lecturer { get; set; }
        public IRoom Room { get; set; }
        public ILectureTime Time { get; set; }
    }

    
}
